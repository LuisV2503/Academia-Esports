using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Academias
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCrea { get; set; }
        public bool Nacional { get; set; }

        public List<Equipos_Academias> EquiposAcademias { get; set; } = new List<Equipos_Academias>();
        public List<Empresas_Academias> EmpresasAcademias { get; set; } = new List<Empresas_Academias>();
        public List<Empresas_Academias_Eventos> EmpresasAcademiasEventos { get; set; } = new List<Empresas_Academias_Eventos>();
    }
}
