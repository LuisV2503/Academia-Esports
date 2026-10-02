namespace libreria_academia.entidades
{
    public class Equipos_Juegos
    {
        public int id { get; set; }

        public int Equipo { get; set; }
        public Equipos _equipos { get; set; }

        public int Juego { get; set; }
        public Juegos _juegos { get; set; }
    }
}
