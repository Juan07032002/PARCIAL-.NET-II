using System.Collections.Generic;
using System.Threading.Tasks;
using vehiculos.Models;

namespace vehiculos.Repository.Interface
{
    public interface IMarcaRepository
    {
        Task<IEnumerable<Marca>> GetAll();
        Task<Marca> GetById(int id);
        Task Add(Marca marca);
        Task Update(Marca marca);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
