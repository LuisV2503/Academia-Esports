using libreria_academia.entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using presentacion_academia.interfaces;

namespace front_academia.Filtros
{
    // Se ejecuta en TODAS las páginas Razor (se registra una sola vez en Program.cs):
    // 1. exige sesión iniciada, 2. comprueba que la cuenta siga habilitada y refresca el rol,
    // 3. aplica los permisos del rol.
    public class FiltroSesion : IAsyncPageFilter
    {
        private const string RolAdministrador = "Administrador";

        // Páginas que solo puede abrir el administrador
        private static readonly string[] PaginasDeAdministrador =
        {
            "/Ventanas/Roles",
            "/Ventanas/Usuarios",
            "/Ventanas/Logins",
            "/Ventanas/Auditorias",
            "/Ventanas/Reportes"
        };

        private ILoginsPresentacion ILoginsPresentacion;
        private IContextoUsuario IContextoUsuario;

        public FiltroSesion(ILoginsPresentacion ILoginsPresentacion, IContextoUsuario IContextoUsuario)
        {
            this.ILoginsPresentacion = ILoginsPresentacion;
            this.IContextoUsuario = IContextoUsuario;
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnPageHandlerExecutionAsync(
            PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            var pagina = context.ActionDescriptor.ViewEnginePath;

            // El login y la página de error se pueden abrir sin sesión
            if (pagina == "/Index" || pagina == "/Error")
            {
                await next();
                return;
            }

            var sesion = context.HttpContext.Session;

            // 1. ¿Hay sesión iniciada?
            var id = sesion.GetString("Usuario");
            if (string.IsNullOrEmpty(id))
            {
                context.Result = new RedirectToPageResult("/Index");
                return;
            }

            // 2. ¿Sigue habilitada la cuenta? ¿Qué rol tiene ahora?
            try
            {
                var usuario = this.ILoginsPresentacion.Estado(
                    new Usuarios() { Id = Convert.ToInt32(id) });
                sesion.SetString("Nombre", usuario.Nombre ?? "");
                sesion.SetString("Rol", usuario._rol?.Nombre ?? "");

                // A partir de aquí, Comunicaciones envía este usuario al back (encabezado X-Usuario)
                this.IContextoUsuario.Usuario = Convert.ToInt32(id);
            }
            catch (Exception ex)
            {
                var error = ex;
                while (error is AggregateException && error.InnerException != null)
                    error = error.InnerException;

                sesion.Clear();
                sesion.SetString("MensajeLogin", error.Message);
                context.Result = new RedirectToPageResult("/Index");
                return;
            }

            // 3. Permisos según el rol
            var esAdministrador = sesion.GetString("Rol") == RolAdministrador;
            if (!esAdministrador)
            {
                if (PaginasDeAdministrador.Contains(pagina))
                {
                    sesion.SetString("MensajeLogin", "No tienes permiso para abrir esa página");
                    context.Result = new RedirectToPageResult("/Index");
                    return;
                }

                // Sin ser administrador solo se puede abrir la página (OnGet) y buscar
                var metodo = context.HandlerMethod?.MethodInfo.Name;
                if (metodo != null && metodo != "OnGet" && metodo != "OnPostBtBuscar")
                {
                    context.Result = new RedirectToPageResult(pagina);
                    return;
                }
            }

            await next();
        }
    }
}
