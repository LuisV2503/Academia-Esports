using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AnalistasController : ControllerBase
    {
        private IAnalistasAplicacion IAnalistasAplicacion;

        public AnalistasController(IAnalistasAplicacion IAnalistasAplicacion)
        {
            this.IAnalistasAplicacion = IAnalistasAplicacion;
        }

        [HttpGet]
        public List<Analistas> Consultar()
        {
            return this.IAnalistasAplicacion.Consultar();
        }

        [HttpPost]
        public Analistas Insertar(Analistas entidad)
        {
            return this.IAnalistasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Analistas Actualizar(Analistas entidad)
        {
            return this.IAnalistasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Analistas Borrar(Analistas entidad)
        {
            return this.IAnalistasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Analistas> Buscar(Analistas entidad)
        {
            return this.IAnalistasAplicacion.Buscar(entidad);
        }
    }
}
