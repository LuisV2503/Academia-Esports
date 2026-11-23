using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages
{
    public class IndexModel : PageModel
    {
        private ILoginsPresentacion ILoginsPresentacion;

        public IndexModel(ILoginsPresentacion ILoginsPresentacion)
        {
            this.ILoginsPresentacion = ILoginsPresentacion;
        }

        public bool EstaLogueado = false;
        public string? NombreUsuario;
        public string? RolUsuario;
        [BindProperty] public string? Correo { get; set; }
        [BindProperty] public string? Contrasena { get; set; }

        public void OnGet()
        {
            CargarSesion();
        }

        public void OnPostBtClean()
        {
            Correo = string.Empty;
            Contrasena = string.Empty;
            ModelState.Clear();
            CargarSesion();
        }

        public IActionResult OnPostBtEnter()
        {
            try
            {
                var usuario = this.ILoginsPresentacion.Validar(new Credenciales()
                {
                    Correo = this.Correo,
                    Contrasena = this.Contrasena
                });

                HttpContext.Session.SetString("Usuario", (usuario.Id ?? 0).ToString());
                HttpContext.Session.SetString("Nombre", usuario.Nombre ?? "");
                HttpContext.Session.SetString("Rol", usuario._rol?.Nombre ?? "");
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }

        public IActionResult OnPostBtClose()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        private void CargarSesion()
        {
            EstaLogueado = !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario"));
            NombreUsuario = HttpContext.Session.GetString("Nombre");
            RolUsuario = HttpContext.Session.GetString("Rol");

            // Mensaje que dejó el filtro (cuenta desactivada, sin permiso...)
            var mensaje = HttpContext.Session.GetString("MensajeLogin");
            if (!string.IsNullOrEmpty(mensaje))
            {
                ViewData["Mensaje"] = mensaje;
                HttpContext.Session.Remove("MensajeLogin");
            }
        }
    }
}
