using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Empresas_Academias_EventosController : ControllerBase
    {
        private IEmpresas_Academias_EventosAplicacion IEmpresas_Academias_EventosAplicacion;

        public Empresas_Academias_EventosController(IEmpresas_Academias_EventosAplicacion IEmpresas_Academias_EventosAplicacion)
        {
            this.IEmpresas_Academias_EventosAplicacion = IEmpresas_Academias_EventosAplicacion;
        }

        [HttpGet]
        public List<Empresas_Academias_Eventos> Consultar()
        {
            return this.IEmpresas_Academias_EventosAplicacion.Consultar();
        }

        [HttpPost]
        public Empresas_Academias_Eventos Insertar(Empresas_Academias_Eventos entidad)
        {
            return this.IEmpresas_Academias_EventosAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Empresas_Academias_Eventos Actualizar(Empresas_Academias_Eventos entidad)
        {
            return this.IEmpresas_Academias_EventosAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Empresas_Academias_Eventos Borrar(Empresas_Academias_Eventos entidad)
        {
            return this.IEmpresas_Academias_EventosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Empresas_Academias_Eventos> Buscar(Empresas_Academias_Eventos entidad)
        {
            return this.IEmpresas_Academias_EventosAplicacion.Buscar(entidad);
        }
    }
}
