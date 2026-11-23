using Microsoft.AspNetCore.Mvc;
using libreria_academia.entidades;
using libreria_academia.interfaces;

namespace servicios_academia.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginsController : ControllerBase
    {
        private ILoginsAplicacion ILoginsAplicacion;

        public LoginsController(ILoginsAplicacion ILoginsAplicacion)
        {
            this.ILoginsAplicacion = ILoginsAplicacion;
        }

        [HttpGet]
        public List<Logins> Consultar()
        {
            return this.ILoginsAplicacion.Consultar();
        }

        [HttpPost]
        public Logins Insertar(Logins entidad)
        {
            return this.ILoginsAplicacion.Insertar(entidad);
        }

        [HttpPut]
        public Logins Actualizar(Logins entidad)
        {
            return this.ILoginsAplicacion.Actualizar(entidad);
        }

        [HttpDelete]
        public Logins Borrar(Logins entidad)
        {
            return this.ILoginsAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<Logins> Buscar(Logins entidad)
        {
            return this.ILoginsAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Usuarios Validar(Credenciales entidad)
        {
            return this.ILoginsAplicacion.Validar(entidad);
        }

        [HttpPost]
        public Usuarios Estado(Usuarios entidad)
        {
            return this.ILoginsAplicacion.Estado(entidad);
        }
    }
}
