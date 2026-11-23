using System.Security.Cryptography;
using System.Text;

namespace libreria_academia.nucleo
{
    public class Seguridad
    {
        // SHA-256 en hexadecimal (64 caracteres, mayúsculas). Coincide con el que calcula el script SQL.
        public static string Hash(string clave)
        {
            return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(clave)));
        }
    }
}
