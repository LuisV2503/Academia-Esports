using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class ContextoUsuario : IContextoUsuario
    {
        public int? Usuario { get; set; }
    }
}
