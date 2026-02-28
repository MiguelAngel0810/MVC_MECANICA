using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Api.mecanica;

[Table("mantenimientos")]
[Index("VehiculoId", Name = "vehiculo_id")]
public partial class Mantenimientos
{
    [Key]
    [Column("mantenimiento_id", TypeName = "int(11)")]
    public int MantenimientoId { get; set; }

    [Column("fecha")]
    public DateOnly Fecha { get; set; }

    [Column("tipo_servicio")]
    [StringLength(100)]
    public string TipoServicio { get; set; } = null!;

    [Column("costo")]
    [Precision(10, 2)]
    public decimal Costo { get; set; }

    [Column("detalle", TypeName = "text")]
    public string? Detalle { get; set; }

    [Column("vehiculo_id", TypeName = "int(11)")]
    public int VehiculoId { get; set; }

    [ForeignKey("VehiculoId")]
    [InverseProperty("Mantenimientos")]
    public virtual Vehiculos Vehiculo { get; set; } = null!;
}
