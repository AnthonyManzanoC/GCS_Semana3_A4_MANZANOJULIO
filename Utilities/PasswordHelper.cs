using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DonJulioSuper.Utilities
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Crea el hash y salt para una contraseña dada.
        /// </summary>
        public static void CreatePasswordHash(string password, out string passwordHash, out string salt)
        {
            // Generar un salt aleatorio de 16 bytes
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            salt = Convert.ToBase64String(saltBytes);

            // Generar el hash usando PBKDF2 con 10000 iteraciones
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(20); // 20 bytes para el hash
                passwordHash = Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Verifica la contraseña ingresada comparándola con el hash almacenado.
        /// </summary>
        public static bool VerifyPassword(string password, string salt, string storedHash)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
            {
                byte[] hashBytes = pbkdf2.GetBytes(20);
                string computedHash = Convert.ToBase64String(hashBytes);
                return computedHash == storedHash;
            }
        }
    }
}