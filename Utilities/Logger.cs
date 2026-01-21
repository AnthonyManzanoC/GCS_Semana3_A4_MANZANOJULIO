using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonJulioSuper.Utilities
{
    public static class Logger
    {
        private static readonly string logFilePath = "log.txt";

        /// <summary>
        /// Registra un mensaje en el log, precedido de la fecha y hora actual.
        /// </summary>
        /// <param name="mensaje">Mensaje a registrar.</param>
        public static void Log(string mensaje)
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensaje}";
                File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // En caso de error, se podría enviar el log a una ubicación alternativa, o notificar al administrador.
                Console.WriteLine("Error al escribir en el log: " + ex.Message);
            }
        }
    }
}