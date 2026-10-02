namespace libreria_academia.entidades
{
    public class Eventos_Organizadores
    {
        public int id { get; set; }

        public int Evento { get; set; }
        public Eventos _eventos { get; set; }

        public int Organizador { get; set; }
        public Organizadores _organizadores { get; set; }
    }
}
