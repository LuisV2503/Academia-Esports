using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Personas
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }

        public int Sueldo { get; set; }
        public Sueldos _sueldo { get; set; }

        // Listas de roles asociados (muchos a 1 hacia Personas)
        public List<Jugadores> Jugadores { get; set; } = new List<Jugadores>();
        public List<Entrenadores> Entrenadores { get; set; } = new List<Entrenadores>();
        public List<Analistas> Analistas { get; set; } = new List<Analistas>();
        public List<Psicologos> Psicologos { get; set; } = new List<Psicologos>();
        public List<Fisioterapeutas> Fisioterapeutas { get; set; } = new List<Fisioterapeutas>();
        public List<Organizadores> Organizadores { get; set; } = new List<Organizadores>();
    }
}
