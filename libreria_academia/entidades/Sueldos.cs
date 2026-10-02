namespace libreria_academia.entidades
{
    public class Sueldos
    {
        public int id { get; set; }
        public int Valor { get; set; }

        // Relación 1 a 1 con Personas
        public Personas _persona { get; set; }
    }
}
