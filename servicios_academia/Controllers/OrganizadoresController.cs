using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class OrganizadoresController : ControllerBase
    {
        private IOrganizadoresAplicacion IOrganizadoresAplicacion;

        public OrganizadoresController(IOrganizadoresAplicacion IOrganizadoresAplicacion)
        {
            this.IOrganizadoresAplicacion = IOrganizadoresAplicacion;
        }

        [HttpGet]
        public List<Organizadores> Consultar()
        {
            return this.IOrganizadoresAplicacion.Consultar();
        }

        [HttpPost]
        public Organizadores Insertar(Organizadores entidad)
        {
            return this.IOrganizadoresAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Organizadores Actualizar(Organizadores entidad)
        {
            return this.IOrganizadoresAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Organizadores Borrar(Organizadores entidad)
        {
            return this.IOrganizadoresAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Organizadores> Buscar(Organizadores entidad)
        {
            return this.IOrganizadoresAplicacion.Buscar(entidad);
        }
    }
}
