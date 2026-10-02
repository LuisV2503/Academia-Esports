namespace libreria_academia.entidades
{
    public class Equipos_Empresas
    {
        public int id { get; set; }

        public int Equipo { get; set; }
        public Equipos _equipos { get; set; }

        public int Empresa { get; set; }
        public Empresas _empresas { get; set; }
    }
}
