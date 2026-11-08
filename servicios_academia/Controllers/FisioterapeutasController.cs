using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FisioterapeutasController : ControllerBase
    {
        private IFisioterapeutasAplicacion IFisioterapeutasAplicacion;

        public FisioterapeutasController(IFisioterapeutasAplicacion IFisioterapeutasAplicacion)
        {
            this.IFisioterapeutasAplicacion = IFisioterapeutasAplicacion;
        }

        [HttpGet]
        public List<Fisioterapeutas> Consultar()
        {
            return this.IFisioterapeutasAplicacion.Consultar();
        }

        [HttpPost]
        public Fisioterapeutas Insertar(Fisioterapeutas entidad)
        {
            return this.IFisioterapeutasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Fisioterapeutas Actualizar(Fisioterapeutas entidad)
        {
            return this.IFisioterapeutasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Fisioterapeutas Borrar(Fisioterapeutas entidad)
        {
            return this.IFisioterapeutasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Fisioterapeutas> Buscar(Fisioterapeutas entidad)
        {
            return this.IFisioterapeutasAplicacion.Buscar(entidad);
        }
    }
}
