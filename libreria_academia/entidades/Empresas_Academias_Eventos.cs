namespace libreria_academia.entidades
{
    public class Empresas_Academias_Eventos
    {
        public int? Id { get; set; }
        public int? Empresa { get; set; }
        public Empresas? _empresas { get; set; }
        public int? Academia { get; set; }
        public Academias? _academias { get; set; }
        public int? Evento { get; set; }
        public Eventos? _eventos { get; set; }
    }
}
