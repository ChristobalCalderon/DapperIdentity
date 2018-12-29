using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interfaces
{
    public interface IBlobCommand
    {
        Task<string> SaveFile(IFormFile file);
    }
}
