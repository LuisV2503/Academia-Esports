using System.Collections.Generic;

namespace libreria_academia.entidades
{
    public class Juegos
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Genero { get; set; }
        public int? CantJug { get; set; }
        public bool? Pago { get; set; }

        public List<Equipos_Juegos> EquiposJuegos { get; set; } = new List<Equipos_Juegos>();
    }
}
