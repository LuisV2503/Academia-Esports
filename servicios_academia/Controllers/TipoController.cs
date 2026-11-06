using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TipoController : ControllerBase
    {
        private ITipoAplicacion ITipoAplicacion;

        public TipoController(ITipoAplicacion ITipoAplicacion)
        {
            this.ITipoAplicacion = ITipoAplicacion;
        }

        [HttpGet]
        public List<Tipo> Consultar()
        {
            return this.ITipoAplicacion.Consultar();
        }

        [HttpPost]
        public Tipo Insertar(Tipo entidad)
        {
            return this.ITipoAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Tipo Actualizar(Tipo entidad)
        {
            return this.ITipoAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Tipo Borrar(Tipo entidad)
        {
            return this.ITipoAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Tipo> Buscar(Tipo entidad)
        {
            return this.ITipoAplicacion.Buscar(entidad);
        }
    }
}
