using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dulce_Sabor_Modulo_Administrador.Models;

public class venta
{
    [Key]
    public int id { get; set; }

    public DateTime fecha { get; set; }

    public string? estado { get; set; }

}
