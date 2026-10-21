using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Equipos
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Siglas { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCrea { get; set; }

        public List<Jugadores> Jugadores { get; set; } = new List<Jugadores>();
        public List<Entrenadores> Entrenadores { get; set; } = new List<Entrenadores>();
        public List<Analistas> Analistas { get; set; } = new List<Analistas>();
        public List<Psicologos> Psicologos { get; set; } = new List<Psicologos>();
        public List<Fisioterapeutas> Fisioterapeutas { get; set; } = new List<Fisioterapeutas>();
        public List<Equipos_Juegos> EquiposJuegos { get; set; } = new List<Equipos_Juegos>();
        public List<Equipos_Empresas> EquiposEmpresas { get; set; } = new List<Equipos_Empresas>();
        public List<Equipos_Academias> EquiposAcademias { get; set; } = new List<Equipos_Academias>();
    }
}
