using DbModel.mecanica;
using DtoModel.Vehiculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.VehiculoRepo.Mapping
{
    public static class VehiculoMapping
    {
        /// <summary>
        /// Cambia el objeto Vehiculo a VehiculoDto para ser utilizado en la capa de servicio o presentación
        /// </summary>
        /// <param name="vehiculo"></param>
        /// <returns></returns>
        public static VehiculoDto ToDto(this Vehiculo vehiculo)
        {
            return new VehiculoDto
            {
                VehiculoId = vehiculo.VehiculoId,
                Placa = vehiculo.Placa,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Anio = vehiculo.Anio
            };
        }

        /// <summary>
        /// Cambia el objeto VehiculoDto a Vehiculo para ser utilizado en la capa de acceso a datos
        /// </summary>
        /// <param name="vehiculoDto"></param>
        /// <returns></returns>
        public static Vehiculo ToEntity(this VehiculoDto vehiculoDto)
        {
            return new Vehiculo
            {
                VehiculoId = vehiculoDto.VehiculoId,
                Placa = vehiculoDto.Placa,
                Marca = vehiculoDto.Marca,
                Modelo = vehiculoDto.Modelo,
                Anio = vehiculoDto.Anio
            };
        }

        public static List<VehiculoDto> ToDtoList(this List<Vehiculo> vehiculos)
        {
            return vehiculos.Select(v => v.ToDto()).ToList();
        }

        public static List<Vehiculo> ToEntityList(this List<VehiculoDto> vehiculoDtos)
        {
            return vehiculoDtos.Select(v => v.ToEntity()).ToList();
        }
    }
}
