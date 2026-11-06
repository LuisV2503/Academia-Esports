using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AcademiasController : ControllerBase
    {
        private IAcademiasAplicacion IAcademiasAplicacion;

        public AcademiasController(IAcademiasAplicacion IAcademiasAplicacion)
        {
            this.IAcademiasAplicacion = IAcademiasAplicacion;
        }

        [HttpGet]
        public List<Academias> Consultar()
        {
            return this.IAcademiasAplicacion.Consultar();
        }

        [HttpPost]
        public Academias Insertar(Academias entidad)
        {
            return this.IAcademiasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Academias Actualizar(Academias entidad)
        {
            return this.IAcademiasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Academias Borrar(Academias entidad)
        {
            return this.IAcademiasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Academias> Buscar(Academias entidad)
        {
            return this.IAcademiasAplicacion.Buscar(entidad);
        }
    }
}
