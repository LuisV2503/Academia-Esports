namespace libreria_academia.entidades
{
    public class Empresas_Academias
    {
        public int id { get; set; }

        public int Empresa { get; set; }
        public Empresas _empresas { get; set; }

        public int Academia { get; set; }
        public Academias _academias { get; set; }
    }
}
