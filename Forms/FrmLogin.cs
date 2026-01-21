using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DonJulioSuper.DataAccess;
using DonJulioSuper.Models;
using DonJulioSuper.Utilities;

namespace DonJulioSuper.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text.Trim();
            string contraseña = txtContraseña.Text.Trim();

            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.");
                return;
            }

            string rol;
            if (AutenticarUsuario(nombreUsuario, contraseña, out rol))
            {
                // Registrar el inicio de sesión
                Logger.Log($"Inicio de sesión exitoso para el usuario: {nombreUsuario}");
                MessageBox.Show("Bienvenido, " + nombreUsuario);

                // Guardar información del usuario en una variable global
                UsuarioActual.NombreUsuario = nombreUsuario;
                UsuarioActual.Rol = rol;

                // Abrir el formulario principal y ocultar el login
                FrmPrincipal frm = new FrmPrincipal();
                frm.Show();
                this.Hide();
            }
            else
            {
                Logger.Log($"Fallo en el inicio de sesión para el usuario: {nombreUsuario}");
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private bool AutenticarUsuario(string nombreUsuario, string contraseña, out string rol)
        {
            rol = string.Empty;
            try
            {
                using (SqlConnection conn = DBConnection.GetConnection())
                {
                    // Consulta actualizada: usamos "Salt" en lugar de "SalSalt"
                    string query = "SELECT Rol, ContraseñaHash, Salt FROM Usuarios WHERE NombreUsuario = @nombreUsuario";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["ContraseñaHash"].ToString();
                                // Leer el valor de "Salt" de la base de datos
                                string storedSalt = reader["Salt"].ToString();

                                // Verificar la contraseña ingresada utilizando el salt y hash almacenados
                                if (PasswordHelper.VerifyPassword(contraseña, storedSalt, storedHash))
                                {
                                    rol = reader["Rol"].ToString();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Registra el error para poder diagnosticar el problema
                Logger.Log($"Error al autenticar el usuario: {nombreUsuario}. Detalle: {ex.Message}");
                MessageBox.Show("Ocurrió un error al intentar iniciar sesión. Inténtelo más tarde.");
            }
            return false;
        }

        private void linkLabelRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistro registroForm = new FrmRegistro();
            registroForm.ShowDialog();
        }

        private void LabelRecuperar_Click(object sender, EventArgs e)
        {
            FrmRecuperarContrasena frmRecuperar = new FrmRecuperarContrasena();
            frmRecuperar.ShowDialog();
        }
    }
}