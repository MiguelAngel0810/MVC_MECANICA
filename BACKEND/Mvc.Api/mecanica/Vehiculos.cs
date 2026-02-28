using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Api.mecanica;

[Table("vehiculos")]
[Index("Placa", Name = "placa", IsUnique = true)]
public partial class Vehiculos
{
    [Key]
    [Column("vehiculo_id", TypeName = "int(11)")]
    public int VehiculoId { get; set; }

    [Column("placa")]
    [StringLength(10)]
    public string Placa { get; set; } = null!;

    [Column("marca")]
    [StringLength(50)]
    public string Marca { get; set; } = null!;

    [Column("modelo")]
    [StringLength(50)]
    public string Modelo { get; set; } = null!;

    [Column("anio", TypeName = "int(11)")]
    public int Anio { get; set; }

    [InverseProperty("Vehiculo")]
    public virtual ICollection<Mantenimientos> Mantenimientos { get; set; } = new List<Mantenimientos>();
}
