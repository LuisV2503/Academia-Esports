using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JugadoresController : ControllerBase
    {
        private IJugadoresAplicacion IJugadoresAplicacion;

        public JugadoresController(IJugadoresAplicacion IJugadoresAplicacion)
        {
            this.IJugadoresAplicacion = IJugadoresAplicacion;
        }

        [HttpGet]
        public List<Jugadores> Consultar()
        {
            return this.IJugadoresAplicacion.Consultar();
        }

        [HttpPost]
        public Jugadores Insertar(Jugadores entidad)
        {
            return this.IJugadoresAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Jugadores Actualizar(Jugadores entidad)
        {
            return this.IJugadoresAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Jugadores Borrar(Jugadores entidad)
        {
            return this.IJugadoresAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Jugadores> Buscar(Jugadores entidad)
        {
            return this.IJugadoresAplicacion.Buscar(entidad);
        }
    }
}
