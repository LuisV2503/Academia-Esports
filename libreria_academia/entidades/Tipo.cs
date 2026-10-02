using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Tipo
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Descripccion { get; set; }

        public List<Empresas> Empresas { get; set; } = new List<Empresas>();
    }
}
