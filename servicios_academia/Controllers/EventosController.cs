using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EventosController : ControllerBase
    {
        private IEventosAplicacion IEventosAplicacion;

        public EventosController(IEventosAplicacion IEventosAplicacion)
        {
            this.IEventosAplicacion = IEventosAplicacion;
        }

        [HttpGet]
        public List<Eventos> Consultar()
        {
            return this.IEventosAplicacion.Consultar();
        }

        [HttpPost]
        public Eventos Insertar(Eventos entidad)
        {
            return this.IEventosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Eventos Actualizar(Eventos entidad)
        {
            return this.IEventosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Eventos Borrar(Eventos entidad)
        {
            return this.IEventosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Eventos> Buscar(Eventos entidad)
        {
            return this.IEventosAplicacion.Buscar(entidad);
        }
    }
}
