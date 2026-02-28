using DtoModel.Vehiculo;
using Mvc.Repository.VehiculoRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mvc.Bussnies.Vehiculo
{
    public class VehiculoBussnies : IVehiculoBussnies
    {
        private readonly IVehiculoRepository _vehiculoRepository;

        public VehiculoBussnies(IVehiculoRepository vehiculoRepository)
        {
            _vehiculoRepository = vehiculoRepository;
        }

        public async Task<VehiculoDto> Create(VehiculoDto request)
        {
            VehiculoDto result = await _vehiculoRepository.Create(request);
            return result;
        }

        public async Task<List<VehiculoDto>> GetAll()
        {
            List<VehiculoDto> lista = await _vehiculoRepository.GetAll();
            return lista;
        }

        public async Task<VehiculoDto?> GetById(int id)
        {
            VehiculoDto vehiculo = await _vehiculoRepository.GetById(id);
            return vehiculo;
        }

        public async Task<VehiculoDto?> Update(VehiculoDto request)
        {
            VehiculoDto? vehiculoDb = await _vehiculoRepository.GetById(request.VehiculoId);

            if (vehiculoDb == null)
            {
                throw new Exception("Vehículo a actualizar no existe");
            }

            VehiculoDto result = await _vehiculoRepository.Update(request);
            return result;
        }

        public async Task Delete(int id)
        {
            await _vehiculoRepository.Delete(id);
        }
    }
}
