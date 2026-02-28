using DbModel.mecanica;
using DtoModel.Vehiculo;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.VehiculoRepo.Contratos;
using Mvc.Repository.VehiculoRepo.Mapping;

namespace Mvc.Repository.VehiculoRepo.Implementacion
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly _mecanicaContext _db;

        //Estamos generando un constructor 
        //este nos permite inicializar cualquier recurso que necesitemos
        //para nuestra clase, como conexiones a bases de datos, servicios externos, etc.
        public VehiculoRepository(_mecanicaContext db)
        {
            _db = db;
        }

        public async Task<VehiculoDto> Create(VehiculoDto request)
        {
            Vehiculo entity = VehiculoMapping.ToEntity(request);
            await _db.Vehiculos.AddAsync(entity);
            await _db.SaveChangesAsync();

            request = VehiculoMapping.ToDto(entity);
            return request;
        }

        public async Task Delete(int id)
        {
            await _db.Vehiculos.Where(x => x.VehiculoId == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<VehiculoDto>> GetAll()
        {
            List<VehiculoDto> res = new List<VehiculoDto>();
            List<Vehiculo> data = await _db.Vehiculos.ToListAsync();
            res = VehiculoMapping.ToDtoList(data);
            return res;
        }

        public async Task<VehiculoDto?> GetById(int id)
        {
            Vehiculo? entity = await _db.Vehiculos.Where(x => x.VehiculoId == id).FirstOrDefaultAsync();
            if (entity == null) { return null; }
            VehiculoDto res = VehiculoMapping.ToDto(entity);
            return res;
        }

        public async Task<VehiculoDto> Update(VehiculoDto request)
        {
            var entity = await _db.Vehiculos.FindAsync(request.VehiculoId);
            try
            {
                // Obtener la entidad existente desde la base de datos
                if (entity == null)
                {
                    throw new Exception("Vehículo no encontrado");
                }

                // Actualizar las propiedades
                entity.Placa = request.Placa;
                entity.Marca = request.Marca;
                entity.Modelo = request.Modelo;
                entity.Anio = request.Anio;

                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return VehiculoMapping.ToDto(entity);
        }
    }
}
