using EfCoreApp.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfCoreApp.Services.Interfaces
{
    public interface ISamuraiServices
    {
        Task<IEnumerable<Samurai>> GetAll();
        Task<Samurai> GetOne(int id);
        Task Add(Samurai samurai);
    }
}