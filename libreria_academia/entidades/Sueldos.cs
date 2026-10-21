

using System.Text.Json.Serialization;

namespace libreria_academia.entidades
{
    public class Sueldos
    {
        public int? Id { get; set; }
        public int? Valor { get; set; }
        [JsonIgnore] public List<Personas> Personas { get; set; } = new List<Personas>();
    }
}
