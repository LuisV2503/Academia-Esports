namespace libreria_academia.entidades
{
    public class Auditorias
    {
        public int? Id { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }

        public int? Usuario { get; set; }
        public Usuarios? _usuario { get; set; }
    }
}
