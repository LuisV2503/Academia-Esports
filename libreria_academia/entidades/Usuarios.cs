namespace libreria_academia.entidades
{
    public class Usuarios
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public bool? Activo { get; set; }
        public int? Rol { get; set; }
        public Roles? _rol { get; set; }
        public string? Imagen { get; set; }
    }
}
