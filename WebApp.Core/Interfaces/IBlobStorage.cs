using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Interfaces
{
    public interface IBlobStorage
    {
        Task<string> SaveFile(IFormFile file);
    }
}
