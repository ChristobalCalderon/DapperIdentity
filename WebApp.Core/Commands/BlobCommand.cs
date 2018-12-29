using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp.Core.Interfaces;

namespace WebApp.Core.Commands
{
    public class BlobCommand : IBlobCommand
    {
        private readonly IBlobStorage _storage;

        public BlobCommand(IBlobStorage storage)
        {
            _storage = storage;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            return await _storage.SaveFile(file);
        }
    }
}
