using libreria_academia.interfaces;

namespace servicios_academia.Servicios
{
    // Lee el encabezado "X-Usuario" que envía el front con el id del usuario en sesión
    public class UsuarioActual : IUsuarioActual
    {
        private IHttpContextAccessor accesor;

        public UsuarioActual(IHttpContextAccessor accesor)
        {
            this.accesor = accesor;
        }

        public int? Id
        {
            get
            {
                var valor = this.accesor.HttpContext?.Request.Headers["X-Usuario"].FirstOrDefault();
                if (int.TryParse(valor, out var id))
                    return id;
                return null;
            }
        }
    }
}
