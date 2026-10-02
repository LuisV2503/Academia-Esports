namespace libreria_academia.entidades
{
    public class Equipos_Academias
    {
        public int id { get; set; }

        public int Equipo { get; set; }
        public Equipos _equipos { get; set; }

        public int Academia { get; set; }
        public Academias _academias { get; set; }
    }
}
