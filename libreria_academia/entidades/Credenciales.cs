namespace libreria_academia.entidades
{
    // No es una tabla: solo transporta el correo y la clave al validar el login
    public class Credenciales
    {
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
    }
}
