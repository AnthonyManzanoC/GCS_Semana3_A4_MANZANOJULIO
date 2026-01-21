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
using DonJulioSuper.Utilities;

namespace DonJulioSuper.Forms
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
            LoadProductos();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadProductos()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "SELECT * FROM Productos";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dt);
            }
            dataGridViewProductos.DataSource = dt;
        }
        private void AgregarProducto(string nombre, string descripcion, decimal precio, int stock)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock) VALUES (@nombre, @descripcion, @precio, @stock)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@precio", precio);
                cmd.Parameters.AddWithValue("@stock", stock);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            LoadProductos();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    string nombre = txtNombre.Text.Trim();
                    string descripcion = txtDescripcion.Text.Trim();
                    decimal precio = decimal.Parse(txtPrecio.Text.Trim());
                    int stock = int.Parse(txtStock.Text.Trim());

                    AgregarProducto(nombre, descripcion, precio, stock);
                    MessageBox.Show("Producto agregado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar producto: " + ex.Message);
                }
            }
    }
        public void ActualizarStock(int productoID, int cantidadVendida)
        {
            Logger.Log($"Stock actualizado para ProductoID: {productoID}. ");

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
