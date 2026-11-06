using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EquiposController : ControllerBase
    {
        private IEquiposAplicacion IEquiposAplicacion;

        public EquiposController(IEquiposAplicacion IEquiposAplicacion)
        {
            this.IEquiposAplicacion = IEquiposAplicacion;
        }

        [HttpGet]
        public List<Equipos> Consultar()
        {
            return this.IEquiposAplicacion.Consultar();
        }

        [HttpPost]
        public Equipos Insertar(Equipos entidad)
        {
            return this.IEquiposAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Equipos Actualizar(Equipos entidad)
        {
            return this.IEquiposAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Equipos Borrar(Equipos entidad)
        {
            return this.IEquiposAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Equipos> Buscar(Equipos entidad)
        {
            return this.IEquiposAplicacion.Buscar(entidad);
        }
    }
}
