namespace libreria_academia.entidades
{
    public class Logins
    {
        public int? Id { get; set; }

        public int? Usuario { get; set; }
        public Usuarios? _usuario { get; set; }
        public string? Contrasena { get; set; }
        public bool? Activo { get; set; }
    }
}
