using DonJulioSuper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DonJulioSuper.Utilities
{
    public static class FacturacionHelper
    {
        public static decimal CalcularSubtotal(List<DetalleFactura> detalles)
    {
        return detalles.Sum(d => d.PrecioUnitario * d.Cantidad);
    }

    public static decimal CalcularImpuestos(decimal subtotal)
    {
        // Ejemplo: 15% de impuestos.
        return subtotal * 0.15m;
    }

    public static decimal CalcularTotal(decimal subtotal, decimal impuestos)
    {
        return subtotal + impuestos;
    }
  }
}