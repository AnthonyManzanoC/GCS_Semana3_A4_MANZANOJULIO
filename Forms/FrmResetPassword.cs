using DonJulioSuper.Utilities;
using DonJulioSuper.DataAccess;
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

namespace DonJulioSuper.Forms
{
    public partial class FrmResetPassword : Form
    {
        private string token;
        private string _token; // Declaración del campo

        public FrmResetPassword(string token)
        {
            InitializeComponent();
            _token = token; // Asignación correcta


            txtToken.ReadOnly = true;
            txtToken.Text = _token; // Mostrar el token en el campo de texto
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtNuevaContraseña.Text.Trim();
            string confirmarContraseña = txtConfirmarContraseña.Text.Trim();

            if (string.IsNullOrEmpty(nuevaContraseña) || string.IsNullOrEmpty(confirmarContraseña))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }
            if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "SELECT UsuarioId, FechaExpiracion FROM RecuperacionContrasena WHERE Token = @token";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@token", _token);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime fechaExpiracion = Convert.ToDateTime(reader["FechaExpiracion"]);
                                if (DateTime.Now > fechaExpiracion)
                                {
                                    MessageBox.Show("El token ha expirado.");
                                    return;
                                }
                                int usuarioId = Convert.ToInt32(reader["UsuarioId"]);
                                reader.Close();

                                // Generar nuevo hash y salt para la nueva contraseña
                                PasswordHelper.CreatePasswordHash(nuevaContraseña, out string newHash, out string newSalt);

                                // Actualizar la contraseña en la tabla Usuarios
                                string updateQuery = "UPDATE Usuarios SET ContraseñaHash = @newHash, Salt = @newSalt WHERE UsuarioID = @usuarioId";
                                using (SqlCommand cmdUpdate = new SqlCommand(updateQuery, conn))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@newHash", newHash);
                                    cmdUpdate.Parameters.AddWithValue("@newSalt", newSalt);
                                    cmdUpdate.Parameters.AddWithValue("@usuarioId", usuarioId);
                                    cmdUpdate.ExecuteNonQuery();
                                }

                                // Eliminar el token después de usarlo
                                string deleteQuery = "DELETE FROM RecuperacionContrasena WHERE Token = @token";
                                using (SqlCommand cmdDelete = new SqlCommand(deleteQuery, conn))
                                {
                                    cmdDelete.Parameters.AddWithValue("@token", _token);
                                    cmdDelete.ExecuteNonQuery();
                                }

                                MessageBox.Show("Contraseña actualizada correctamente. Será redirigido al inicio de sesión.");

                                // **Cerrar todo y volver a FrmLogin**
                                this.Hide();
                                foreach (Form frm in Application.OpenForms.Cast<Form>().ToList())
                                {
                                    if (frm.Name != "FrmLogin") frm.Close();
                                }
                                FrmLogin loginForm = new FrmLogin();
                                loginForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Token inválido.");
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error al resetear contraseña: {ex.Message}");
                MessageBox.Show("Ocurrió un error. Intente nuevamente.");
            }
        }
    }


       
}