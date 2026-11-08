using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EntrenadoresController : ControllerBase
    {
        private IEntrenadoresAplicacion IEntrenadoresAplicacion;

        public EntrenadoresController(IEntrenadoresAplicacion IEntrenadoresAplicacion)
        {
            this.IEntrenadoresAplicacion = IEntrenadoresAplicacion;
        }

        [HttpGet]
        public List<Entrenadores> Consultar()
        {
            return this.IEntrenadoresAplicacion.Consultar();
        }

        [HttpPost]
        public Entrenadores Insertar(Entrenadores entidad)
        {
            return this.IEntrenadoresAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Entrenadores Actualizar(Entrenadores entidad)
        {
            return this.IEntrenadoresAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Entrenadores Borrar(Entrenadores entidad)
        {
            return this.IEntrenadoresAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Entrenadores> Buscar(Entrenadores entidad)
        {
            return this.IEntrenadoresAplicacion.Buscar(entidad);
        }
    }
}
