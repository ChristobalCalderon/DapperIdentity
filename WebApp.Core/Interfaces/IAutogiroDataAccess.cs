using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IAutogiroDataAccess
    {
        Task<bool> AddAsync(Autogiro giro);
    }
}