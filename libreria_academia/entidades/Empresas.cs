using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Empresas
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Pais { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCrea { get; set; }
        public string? Correo { get; set; }
        public int? Tipo { get; set; }
        public Tipo? _tipo { get; set; }

        public List<Equipos_Empresas> EquiposEmpresas { get; set; } = new List<Equipos_Empresas>();
        public List<Empresas_Academias> EmpresasAcademias { get; set; } = new List<Empresas_Academias>();
        public List<Empresas_Academias_Eventos> EmpresasAcademiasEventos { get; set; } = new List<Empresas_Academias_Eventos>();
    }
}
