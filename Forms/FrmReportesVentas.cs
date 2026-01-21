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
    public partial class FrmReportesVentas : Form
    {
        public FrmReportesVentas()
        {
            InitializeComponent(); CargarReporteVentas();
        }

        private void CargarReporteVentas()
        {
            DataTable dtVentas = new DataTable();
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = @"
                    SELECT CAST(Fecha AS DATE) AS Fecha, SUM(Total) AS TotalVentas
                    FROM Facturas
                    GROUP BY CAST(Fecha AS DATE)
                    ORDER BY Fecha DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.Fill(dtVentas);
            }
            dataGridViewVentas.DataSource = dtVentas;
        }
        

        private void FrmReportesVentas_Load(object sender, EventArgs e)
        {

        }
    }
}
