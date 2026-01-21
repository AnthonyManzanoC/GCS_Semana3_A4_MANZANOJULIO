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
    public partial class FrmRegistro : Form
    {
        public FrmRegistro()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();
            string confirmarContraseña = txtConfirmar.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña) ||
                string.IsNullOrEmpty(confirmarContraseña) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            if (contraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            // Generar hash y salt de la contraseña
            PasswordHelper.CreatePasswordHash(contraseña, out string contraseñaHash, out string salt);

            // Insertar el usuario en la base de datos
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "INSERT INTO Usuarios (NombreUsuario, ContraseñaHash, Salt, Email, Rol) " +
               "VALUES (@nombreUsuario, @contraseñaHash, @salt, @email, @rol)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@contraseñaHash", contraseñaHash);
                        cmd.Parameters.AddWithValue("@salt", salt);
                        cmd.Parameters.AddWithValue("@email", email);
                        // Asignar un rol permitido, por ejemplo "Vendedor"
                        cmd.Parameters.AddWithValue("@rol", "Vendedor");

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Registro exitoso. Ahora puedes iniciar sesión.");
                        this.Close(); // Cerrar formulario de registro
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar el usuario: " + ex.Message);
            }
        }
    }
  }
