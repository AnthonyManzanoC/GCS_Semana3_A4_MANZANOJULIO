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
    public partial class FrmRecuperarContrasena : Form
    {
        public FrmRecuperarContrasena()
        {
            InitializeComponent();
        }

        
       private void btnEnviar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Ingrese su email.");
                return;
            }

            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    string query = "SELECT UsuarioID FROM Usuarios WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        conn.Open();
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            int usuarioId = Convert.ToInt32(result);

                            // Generar token y fecha de expiración (1 hora)
                            string token = Guid.NewGuid().ToString();
                            DateTime fechaExpiracion = DateTime.Now.AddHours(1);

                            // Insertar el token en la tabla de recuperación
                            string insertQuery = "INSERT INTO RecuperacionContrasena (UsuarioId, Token, FechaExpiracion) " +
                                                 "VALUES (@usuarioId, @token, @fechaExpiracion)";
                            using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@usuarioId", usuarioId);
                                cmdInsert.Parameters.AddWithValue("@token", token);
                                cmdInsert.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
                                cmdInsert.ExecuteNonQuery();
                            }

                            // Enviar el token por email
                            string subject = "Recuperación de contraseña";
                            string body = $"Utilice este token para restablecer su contraseña:\n{token}";
                            EmailHelper.SendEmail(email, subject, body);

                            MessageBox.Show("Se han enviado las instrucciones a su email.");

                            // **Abrir automáticamente FrmResetPassword con el token cargado**
                            FrmResetPassword frmReset = new FrmResetPassword(token);
                            this.Hide();
                            frmReset.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un usuario con ese email.");
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Error en recuperación de contraseña: {ex.Message}");
                MessageBox.Show("Ocurrió un error al procesar su solicitud. Inténtelo más tarde.");
            }
        }
    }
}
