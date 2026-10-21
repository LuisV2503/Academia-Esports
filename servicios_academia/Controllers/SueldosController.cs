using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SueldosController : ControllerBase
    {
        private ISueldosAplicacion ISueldosAplicacion;

        public SueldosController(ISueldosAplicacion ISueldosAplicacion)
        {
            this.ISueldosAplicacion = ISueldosAplicacion;
        }

        [HttpGet]
        public List<Sueldos> Consultar()
        {
            return this.ISueldosAplicacion.Consultar();
        }

        [HttpPost]
        public Sueldos Insertar(Sueldos entidad)
        {
            return this.ISueldosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Sueldos Actualizar(Sueldos entidad)
        {
            return this.ISueldosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Sueldos Borrar(Sueldos entidad)
        {
            return this.ISueldosAplicacion.Borrar(entidad);
        }
    }
}
