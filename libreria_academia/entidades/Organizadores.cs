using System;
using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Organizadores
    {
        public int? Id { get; set; }
        public bool? Activo { get; set; }
        public int? Persona { get; set; }
        public Personas? _persona { get; set; }

        public List<Eventos_Organizadores> EventosOrganizadores { get; set; } = new List<Eventos_Organizadores>();
    }
}
