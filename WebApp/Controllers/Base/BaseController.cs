using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly IConfiguration _configuration;

        public BaseController()
        {

        }

        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async Task<string> GenerateJwtToken(string userId, IList<string> role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, string.Join(',', role.ToArray()))
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected string GetClaimValue(string claimType)
        {
            var jwt = Request.Headers["Authorization"];
            var token = JsonConvert.SerializeObject(jwt.First().Split(' ')[1]);
            token = token.TrimStart('"').TrimEnd('"');
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            var userID = tokenS.Claims.First(claim => claim.Type == claimType).Value;
            return userID;
        }
    }
}
