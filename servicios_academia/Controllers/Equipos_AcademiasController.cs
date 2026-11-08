using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Equipos_AcademiasController : ControllerBase
    {
        private IEquipos_AcademiasAplicacion IEquipos_AcademiasAplicacion;

        public Equipos_AcademiasController(IEquipos_AcademiasAplicacion IEquipos_AcademiasAplicacion)
        {
            this.IEquipos_AcademiasAplicacion = IEquipos_AcademiasAplicacion;
        }

        [HttpGet]
        public List<Equipos_Academias> Consultar()
        {
            return this.IEquipos_AcademiasAplicacion.Consultar();
        }

        [HttpPost]
        public Equipos_Academias Insertar(Equipos_Academias entidad)
        {
            return this.IEquipos_AcademiasAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Equipos_Academias Actualizar(Equipos_Academias entidad)
        {
            return this.IEquipos_AcademiasAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Equipos_Academias Borrar(Equipos_Academias entidad)
        {
            return this.IEquipos_AcademiasAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Equipos_Academias> Buscar(Equipos_Academias entidad)
        {
            return this.IEquipos_AcademiasAplicacion.Buscar(entidad);
        }
    }
}
