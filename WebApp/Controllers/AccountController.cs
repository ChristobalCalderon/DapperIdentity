using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core;
using WebApp.Core.Models.ViewModels;
using WebApp.Core.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebApp.Controllers.Base;
using Serilog;

namespace WebApp.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration) : base(configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var role = await _userManager.GetRolesAsync(user);
                return Ok(new { jwt = await GenerateJwtToken(user.Id, role) });
            }

            return BadRequest(new { message = "An error occured" });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var passworddHaser = _userManager.PasswordHasher;

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Email.ToUpper(),
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = "070",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            };

            user.PasswordHash = passworddHaser.HashPassword(user, model.ConfirmPassword);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                user = await _userManager.FindByEmailAsync(model.Email);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                string FilePath = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Smtp\\Template\\activate.html").LocalPath;
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("[acitvationLink]", token);
                MailText = MailText.Replace("[tsurl]", _configuration["BaseUrlClient"]);


                await _emailSender.SendActivationMail(user.Email, user.UserName, token, MailText);

                return Ok();
            }

            return BadRequest(new { message = "An error occured" });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                    return BadRequest(new { message = "Invalid Email" });

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                string FilePath = "Smtp\\Template\\resetpassword.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("[firstname]", user.UserName);
                MailText = MailText.Replace("[resetcode]", token);
                MailText = MailText.Replace("[tsurl]", _configuration["BaseUrlClient"]);


                await _emailSender.SendActivationMail(user.Email, user.UserName, token, MailText);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ActivateAccount(ActivateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        throw new ApplicationException($"Unable to load user with ID '{model.Email}'.");
                    }

                    var result = await _userManager.ConfirmEmailAsync(user, model.Code);

                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Can't activate account for { model.Email } with code { model.Code } ex: { ex.Message }");
                }
            }

            return BadRequest(new { message = "An error occured" });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody]string currentPassword, [FromBody] string newPassword)
        {
            var userId = GetClaimValue("sub");

            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{ userId }'.");
                }

                var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Couldn't change password for { userId } ex: { ex.Message }");
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeForgottenPassword([FromBody]ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            try
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Couldn't change password for { user.Id } ex: { ex.Message }");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromBody]string email)
        {
            return Ok(await _userManager.FindByEmailAsync(email));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody]LoginViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Email, false);

            if (result.Succeeded)
            {
                var resultDelete = await _userManager.DeleteAsync(user);
                if (resultDelete.Succeeded)
                {
                    return Ok();

                }
            }

            return BadRequest();
        }
    }
}
