namespace libreria_academia.entidades
{
    public class Entretenimientos
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }

        public int Evento { get; set; }
        public Eventos _eventos { get; set; }
    }
}
