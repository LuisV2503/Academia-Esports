using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EntretenimientosController : ControllerBase
    {
        private IEntretenimientosAplicacion IEntretenimientosAplicacion;

        public EntretenimientosController(IEntretenimientosAplicacion IEntretenimientosAplicacion)
        {
            this.IEntretenimientosAplicacion = IEntretenimientosAplicacion;
        }

        [HttpGet]
        public List<Entretenimientos> Consultar()
        {
            return this.IEntretenimientosAplicacion.Consultar();
        }

        [HttpPost]
        public Entretenimientos Insertar(Entretenimientos entidad)
        {
            return this.IEntretenimientosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Entretenimientos Actualizar(Entretenimientos entidad)
        {
            return this.IEntretenimientosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Entretenimientos Borrar(Entretenimientos entidad)
        {
            return this.IEntretenimientosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Entretenimientos> Buscar(Entretenimientos entidad)
        {
            return this.IEntretenimientosAplicacion.Buscar(entidad);
        }
    }
}
