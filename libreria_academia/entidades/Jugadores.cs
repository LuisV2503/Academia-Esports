using System;

namespace libreria_academia.entidades
{
    public class Jugadores
    {
        public int? Id { get; set; }
        public bool? Activo { get; set; }
        public int? Persona { get; set; }
        public Personas? _persona { get; set; }
        public int? Equipo { get; set; }
        public Equipos? _equipo { get; set; }
    }
}
