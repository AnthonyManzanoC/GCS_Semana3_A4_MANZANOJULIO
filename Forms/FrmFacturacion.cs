using DonJulioSuper.Models;
using DonJulioSuper.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DonJulioSuper.DataAccess;
namespace DonJulioSuper.Forms
{
    public partial class FrmFacturacion : Form
    {
        private List<DetalleFactura> carrito = new List<DetalleFactura>();  
        public FrmFacturacion()
        {
            InitializeComponent();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                int productoID = int.Parse(txtProductoID.Text.Trim());
                string nombre = txtNombreProducto.Text.Trim();
                int cantidad = int.Parse(txtCantidad.Text.Trim());
                decimal precio = decimal.Parse(txtPrecioUnitario.Text.Trim());

                carrito.Add(new DetalleFactura
                {
                    ProductoID = productoID,
                    NombreProducto = nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = precio
                });

                ActualizarCarritoUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        // Refresca el DataGridView del carrito.
        private void ActualizarCarritoUI()
        {
            dataGridViewCarrito.DataSource = null;
            dataGridViewCarrito.DataSource = carrito;
        }

        private void btnRegistrarFactura_Click(object sender, EventArgs e)
        {
        
        {
            try
            {
                int clienteID = int.Parse(txtClienteID.Text.Trim());
                int usuarioID = int.Parse(txtUsuarioID.Text.Trim()); // ID del usuario logueado

                // Calcular totales.
                decimal subtotal = FacturacionHelper.CalcularSubtotal(carrito);
                decimal impuestos = FacturacionHelper.CalcularImpuestos(subtotal);
                decimal total = FacturacionHelper.CalcularTotal(subtotal, impuestos);

                // Registrar la factura mediante el procedimiento almacenado.
                int facturaID = RegistrarFactura(clienteID, subtotal, impuestos, total, usuarioID);

                // Registrar cada detalle y actualizar stock.
                foreach (var detalle in carrito)
                {
                    RegistrarDetalleFactura(facturaID, detalle);
                    ActualizarStock(detalle.ProductoID, detalle.Cantidad);
                }

                MessageBox.Show("Factura registrada correctamente. ID Factura: " + facturaID);
                carrito.Clear();
                ActualizarCarritoUI();
                }
                catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la factura: " + ex.Message);
            }
            }
        }
        // Método para registrar la factura y obtener el ID generado.
        private int RegistrarFactura(int clienteID, decimal subtotal, decimal impuestos, decimal total, int usuarioID)
        {
            int facturaID = 0;
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarFactura", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ClienteID", clienteID);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@Impuestos", impuestos);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                SqlParameter outputIdParam = new SqlParameter("@FacturaID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                conn.Open();
                cmd.ExecuteNonQuery();
                facturaID = (int)outputIdParam.Value;
                conn.Close();
            }
            return facturaID;
        }

        // Método para registrar cada detalle de la factura.
        private void RegistrarDetalleFactura(int facturaID, DetalleFactura detalle)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "INSERT INTO DetalleFactura (FacturaID, ProductoID, Cantidad, PrecioUnitario) VALUES (@facturaID, @productoID, @cantidad, @precioUnitario)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@facturaID", facturaID);
                cmd.Parameters.AddWithValue("@productoID", detalle.ProductoID);
                cmd.Parameters.AddWithValue("@cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@precioUnitario", detalle.PrecioUnitario);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        // Método auxiliar para actualizar stock.
        private void ActualizarStock(int productoID, int cantidadVendida)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "UPDATE Productos SET Stock = Stock - @cantidad WHERE ProductoID = @productoID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cantidad", cantidadVendida);
                cmd.Parameters.AddWithValue("@productoID", productoID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
