using System;
using System.Collections.Generic;

namespace Dulce_Sabor_Modulo_Administrador.Models;

public partial class Venta
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();
}
