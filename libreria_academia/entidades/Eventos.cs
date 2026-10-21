using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Eventos
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Activo { get; set; }
        public string? Modalidad { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }

        public List<Empresas_Academias_Eventos> EmpresasAcademiasEventos { get; set; } = new List<Empresas_Academias_Eventos>();
        public List<Eventos_Organizadores> EventosOrganizadores { get; set; } = new List<Eventos_Organizadores>();
        public List<Entretenimientos> Entretenimientos { get; set; } = new List<Entretenimientos>();
    }
}
