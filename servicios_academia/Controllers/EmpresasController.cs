using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmpresasController : ControllerBase
    {
        private IEmpresasAplicacion IEmpresasAplicacion;

        public EmpresasController(IEmpresasAplicacion IEmpresasAplicacion)
        {
            this.IEmpresasAplicacion = IEmpresasAplicacion;
        }

        [HttpGet]
        public List<Empresas> Consultar()
        {
            return this.IEmpresasAplicacion.Consultar();
        }

        [HttpPost]
        public Empresas Insertar(Empresas entidad)
        {
            return this.IEmpresasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Empresas Actualizar(Empresas entidad)
        {
            return this.IEmpresasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Empresas Borrar(Empresas entidad)
        {
            return this.IEmpresasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Empresas> Buscar(Empresas entidad)
        {
            return this.IEmpresasAplicacion.Buscar(entidad);
        }
    }
}
