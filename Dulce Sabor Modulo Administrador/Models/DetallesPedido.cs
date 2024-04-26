using System;
using System.Collections.Generic;

namespace Dulce_Sabor_Modulo_Administrador.Models;

public partial class DetallesPedido
{
    public int IdDetallePedido { get; set; }

    public int? IdVenta { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public double? Precio { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
