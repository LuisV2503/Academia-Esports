using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Empresas_AcademiasController : ControllerBase
    {
        private IEmpresas_AcademiasAplicacion IEmpresas_AcademiasAplicacion;

        public Empresas_AcademiasController(IEmpresas_AcademiasAplicacion IEmpresas_AcademiasAplicacion)
        {
            this.IEmpresas_AcademiasAplicacion = IEmpresas_AcademiasAplicacion;
        }

        [HttpGet]
        public List<Empresas_Academias> Consultar()
        {
            return this.IEmpresas_AcademiasAplicacion.Consultar();
        }

        [HttpPost]
        public Empresas_Academias Insertar(Empresas_Academias entidad)
        {
            return this.IEmpresas_AcademiasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Empresas_Academias Actualizar(Empresas_Academias entidad)
        {
            return this.IEmpresas_AcademiasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Empresas_Academias Borrar(Empresas_Academias entidad)
        {
            return this.IEmpresas_AcademiasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Empresas_Academias> Buscar(Empresas_Academias entidad)
        {
            return this.IEmpresas_AcademiasAplicacion.Buscar(entidad);
        }
    }
}
