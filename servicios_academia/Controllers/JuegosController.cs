using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class JuegosController : ControllerBase
    {
        private IJuegosAplicacion IJuegosAplicacion;

        public JuegosController(IJuegosAplicacion IJuegosAplicacion)
        {
            this.IJuegosAplicacion = IJuegosAplicacion;
        }

        [HttpGet]
        public List<Juegos> Consultar()
        {
            return this.IJuegosAplicacion.Consultar();
        }

        [HttpPost]
        public Juegos Insertar(Juegos entidad)
        {
            return this.IJuegosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Juegos Actualizar(Juegos entidad)
        {
            return this.IJuegosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Juegos Borrar(Juegos entidad)
        {
            return this.IJuegosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Juegos> Buscar(Juegos entidad)
        {
            return this.IJuegosAplicacion.Buscar(entidad);
        }
    }
}
