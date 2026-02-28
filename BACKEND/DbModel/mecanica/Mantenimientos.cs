using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DbModel.mecanica
{
    [Table("mantenimientos")]
    [Index("VehiculoId", Name = "fk_mantenimiento_vehiculo")]
    public partial class Mantenimiento
    {
        [Key]
        [Column("mantenimiento_id")]
        public int MantenimientoId { get; set; }

        [Column("fecha", TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column("tipo_servicio")]
        [StringLength(100)]
        public string TipoServicio { get; set; } = string.Empty;

        [Column("costo", TypeName = "decimal(10,2)")]
        public decimal Costo { get; set; }

        [Column("detalle")]
        public string? Detalle { get; set; }

        [Column("vehiculo_id")]
        public int VehiculoId { get; set; }

        // Relación con Vehiculo
        [ForeignKey("VehiculoId")]
        [InverseProperty("Mantenimientos")]
        public virtual Vehiculo Vehiculo { get; set; } = null!;
    }
}
