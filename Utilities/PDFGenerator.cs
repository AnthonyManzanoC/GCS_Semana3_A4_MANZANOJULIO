
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace DonJulioSuper.Utilities
{
    public static class PDFGenerator
    {
        public static void GenerarFacturaPDF(int facturaID, string rutaArchivo, DataTable dtFactura, DataTable dtDetalles)
        {
            // dtFactura: contiene los datos generales de la factura.
            // dtDetalles: contiene la lista de productos y totales.

            Document documento = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
            documento.Open();

            // Título.
            Paragraph titulo = new Paragraph("Factura #" + facturaID, iTextSharp.text.FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            documento.Add(titulo);
            documento.Add(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy")));
            documento.Add(new Paragraph(" ")); // Espacio.

            // Tabla de Detalles (por ejemplo: Producto, Cantidad, Precio Unitario y Total).
            PdfPTable tabla = new PdfPTable(4);
            tabla.WidthPercentage = 100;
            tabla.AddCell("Producto");
            tabla.AddCell("Cantidad");
            tabla.AddCell("Precio Unitario");
            tabla.AddCell("Total");

            foreach (DataRow row in dtDetalles.Rows)
            {
                tabla.AddCell(row["NombreProducto"].ToString());
                tabla.AddCell(row["Cantidad"].ToString());
                tabla.AddCell(row["PrecioUnitario"].ToString());
                decimal totalLinea = decimal.Parse(row["Cantidad"].ToString()) * decimal.Parse(row["PrecioUnitario"].ToString());
                tabla.AddCell(totalLinea.ToString("F2"));
            }
            documento.Add(tabla);

            // Totales.
            documento.Add(new Paragraph(" "));
            documento.Add(new Paragraph("Subtotal: " + dtFactura.Rows[0]["Subtotal"].ToString()));
            documento.Add(new Paragraph("Impuestos: " + dtFactura.Rows[0]["Impuestos"].ToString()));
            documento.Add(new Paragraph("Total: " + dtFactura.Rows[0]["Total"].ToString()));

            documento.Close();
        }
    }
}
