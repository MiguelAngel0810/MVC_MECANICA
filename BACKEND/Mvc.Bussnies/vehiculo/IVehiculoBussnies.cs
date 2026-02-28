using DtoModel.Vehiculo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mvc.Bussnies.Vehiculo
{
    public interface IVehiculoBussnies
    {
        Task<List<VehiculoDto>> GetAll();
        Task<VehiculoDto?> GetById(int id);
        Task<VehiculoDto> Create(VehiculoDto request);
        Task<VehiculoDto?> Update(VehiculoDto request);
        Task Delete(int id);
    }
}
