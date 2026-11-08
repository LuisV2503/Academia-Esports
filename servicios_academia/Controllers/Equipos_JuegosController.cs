using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Equipos_JuegosController : ControllerBase
    {
        private IEquipos_JuegosAplicacion IEquipos_JuegosAplicacion;

        public Equipos_JuegosController(IEquipos_JuegosAplicacion IEquipos_JuegosAplicacion)
        {
            this.IEquipos_JuegosAplicacion = IEquipos_JuegosAplicacion;
        }

        [HttpGet]
        public List<Equipos_Juegos> Consultar()
        {
            return this.IEquipos_JuegosAplicacion.Consultar();
        }

        [HttpPost]
        public Equipos_Juegos Insertar(Equipos_Juegos entidad)
        {
            return this.IEquipos_JuegosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Equipos_Juegos Actualizar(Equipos_Juegos entidad)
        {
            return this.IEquipos_JuegosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Equipos_Juegos Borrar(Equipos_Juegos entidad)
        {
            return this.IEquipos_JuegosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Equipos_Juegos> Buscar(Equipos_Juegos entidad)
        {
            return this.IEquipos_JuegosAplicacion.Buscar(entidad);
        }
    }
}
