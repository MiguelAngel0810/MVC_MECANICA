using DtoModel.Vehiculo;
using Mvc.Repository.General.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.VehiculoRepo.Contratos
{
    /* RECUERDEN QUE LAS INTERFACES SOLO SIRVEN PARA DECLARAR LOS MÉTODOS A IMPLEMENTAR */

    // IDisposable => garbage collector, para liberar recursos no administrados, como conexiones a bases de datos, archivos, etc.

    // Heredando ICrudRepository<T>
    public interface IVehiculoRepository : ICrudRepository<VehiculoDto>, IDisposable
    {
        // Aquí puedes agregar métodos específicos de Vehiculo si los necesitas
        // Ejemplo: Task<VehiculoDto?> GetByPlaca(string placa);
    }
}
