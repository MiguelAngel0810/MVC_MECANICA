using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.mecanica
{
    [Table("vehiculos")]
    public partial class Vehiculo
    {
        [Key]
        [Column("vehiculo_id")]
        public int VehiculoId { get; set; }

        [Column("placa")]
        [StringLength(10)]
        public string Placa { get; set; } = string.Empty;

        [Column("marca")]
        [StringLength(50)]
        public string Marca { get; set; } = string.Empty;

        [Column("modelo")]
        [StringLength(50)]
        public string Modelo { get; set; } = string.Empty;

        [Column("anio")]
        public int Anio { get; set; }

        // Relación con Mantenimientos
        [InverseProperty("Vehiculo")]
        public virtual ICollection<Mantenimiento> Mantenimientos { get; set; } = new List<Mantenimiento>();
    }
}
