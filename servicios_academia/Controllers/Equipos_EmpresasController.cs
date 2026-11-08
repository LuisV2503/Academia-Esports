using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Equipos_EmpresasController : ControllerBase
    {
        private IEquipos_EmpresasAplicacion IEquipos_EmpresasAplicacion;

        public Equipos_EmpresasController(IEquipos_EmpresasAplicacion IEquipos_EmpresasAplicacion)
        {
            this.IEquipos_EmpresasAplicacion = IEquipos_EmpresasAplicacion;
        }

        [HttpGet]
        public List<Equipos_Empresas> Consultar()
        {
            return this.IEquipos_EmpresasAplicacion.Consultar();
        }

        [HttpPost]
        public Equipos_Empresas Insertar(Equipos_Empresas entidad)
        {
            return this.IEquipos_EmpresasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Equipos_Empresas Actualizar(Equipos_Empresas entidad)
        {
            return this.IEquipos_EmpresasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Equipos_Empresas Borrar(Equipos_Empresas entidad)
        {
            return this.IEquipos_EmpresasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Equipos_Empresas> Buscar(Equipos_Empresas entidad)
        {
            return this.IEquipos_EmpresasAplicacion.Buscar(entidad);
        }
    }
}
