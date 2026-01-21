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
    public partial class FrmCaja : Form
    {
        public FrmCaja()
        {
            InitializeComponent();
        }

        private void btnApertura_Click(object sender, EventArgs e)
        {
            try
            {
                decimal montoApertura = decimal.Parse(txtMontoApertura.Text.Trim());
                AbrirCaja(montoApertura);
                MessageBox.Show("Caja abierta correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir caja: " + ex.Message);
            }
    }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            try
            {
                int cajaID = int.Parse(txtCajaID.Text.Trim());
                decimal montoCierre = decimal.Parse(txtMontoCierre.Text.Trim());
                CerrarCaja(cajaID, montoCierre);
                MessageBox.Show("Caja cerrada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cerrar caja: " + ex.Message);
            }
        }

        private void AbrirCaja(decimal montoApertura)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "INSERT INTO Caja (FechaApertura, MontoApertura) VALUES (GETDATE(), @montoApertura)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@montoApertura", montoApertura);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void CerrarCaja(int cajaID, decimal montoCierre)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "UPDATE Caja SET FechaCierre = GETDATE(), MontoCierre = @montoCierre WHERE CajaID = @cajaID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@montoCierre", montoCierre);
                cmd.Parameters.AddWithValue("@cajaID", cajaID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
