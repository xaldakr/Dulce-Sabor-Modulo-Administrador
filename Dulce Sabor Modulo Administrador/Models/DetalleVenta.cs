using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dulce_Sabor_Modulo_Administrador.Models;

public class detalleventa
{
    [Key]
    public int id_detalle_venta { get; set; }

    public int? id_venta { get; set; }

    public DateTime? fecha_creacion { get; set; }

    public double? precio { get; set; }

}
