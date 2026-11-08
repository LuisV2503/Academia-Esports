using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Eventos_OrganizadoresController : ControllerBase
    {
        private IEventos_OrganizadoresAplicacion IEventos_OrganizadoresAplicacion;

        public Eventos_OrganizadoresController(IEventos_OrganizadoresAplicacion IEventos_OrganizadoresAplicacion)
        {
            this.IEventos_OrganizadoresAplicacion = IEventos_OrganizadoresAplicacion;
        }

        [HttpGet]
        public List<Eventos_Organizadores> Consultar()
        {
            return this.IEventos_OrganizadoresAplicacion.Consultar();
        }

        [HttpPost]
        public Eventos_Organizadores Insertar(Eventos_Organizadores entidad)
        {
            return this.IEventos_OrganizadoresAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Eventos_Organizadores Actualizar(Eventos_Organizadores entidad)
        {
            return this.IEventos_OrganizadoresAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Eventos_Organizadores Borrar(Eventos_Organizadores entidad)
        {
            return this.IEventos_OrganizadoresAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Eventos_Organizadores> Buscar(Eventos_Organizadores entidad)
        {
            return this.IEventos_OrganizadoresAplicacion.Buscar(entidad);
        }
    }
}
