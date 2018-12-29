using System.Threading.Tasks;
using WebApp.Core.Models;

namespace WebApp.Core.Interfaces
{
    public interface IAutogiroCommand
    {
        Task<bool> StoreAutogiroAsync(Autogiro giro);
    }
}