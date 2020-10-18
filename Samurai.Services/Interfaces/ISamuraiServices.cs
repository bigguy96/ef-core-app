using System.Collections.Generic;
using System.Threading.Tasks;
using Samurai.Domain;

namespace Samurai.Services.Interfaces
{
    public interface ISamuraiServices
    {
        Task<IEnumerable<Domain.Samurai>> GetAll();
        Task<Domain.Samurai> GetOne(int id);
        Task Add(Domain.Samurai samurai);
    }
}