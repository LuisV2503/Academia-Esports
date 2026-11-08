using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PsicologosController : ControllerBase
    {
        private IPsicologosAplicacion IPsicologosAplicacion;

        public PsicologosController(IPsicologosAplicacion IPsicologosAplicacion)
        {
            this.IPsicologosAplicacion = IPsicologosAplicacion;
        }

        [HttpGet]
        public List<Psicologos> Consultar()
        {
            return this.IPsicologosAplicacion.Consultar();
        }

        [HttpPost]
        public Psicologos Insertar(Psicologos entidad)
        {
            return this.IPsicologosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Psicologos Actualizar(Psicologos entidad)
        {
            return this.IPsicologosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Psicologos Borrar(Psicologos entidad)
        {
            return this.IPsicologosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Psicologos> Buscar(Psicologos entidad)
        {
            return this.IPsicologosAplicacion.Buscar(entidad);
        }
    }
}
