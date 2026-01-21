using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonJulioSuper.DataAccess
{
    public static class DBConnection
    {
        // Cadena de conexión definida en código.
        // Reemplaza TU_SERVIDOR, DonJulioDB, TU_USUARIO y TU_CONTRASEÑA por tus datos.
        private static readonly string connectionString = "Server=DESKTOP-UQTS9VE\\SQLEXPRESS;Database=DonJulioDB;User Id=sa;Password=123;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}