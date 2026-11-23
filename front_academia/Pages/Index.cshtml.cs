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
        private IEquiposPresentacion IEquiposPresentacion;
        private IJugadoresPresentacion IJugadoresPresentacion;
        private IEntrenadoresPresentacion IEntrenadoresPresentacion;
        private IJuegosPresentacion IJuegosPresentacion;
        private IEmpresasPresentacion IEmpresasPresentacion;
        private IAcademiasPresentacion IAcademiasPresentacion;
        private IEventosPresentacion IEventosPresentacion;

        public IndexModel(
            ILoginsPresentacion ILoginsPresentacion,
            IEquiposPresentacion IEquiposPresentacion,
            IJugadoresPresentacion IJugadoresPresentacion,
            IEntrenadoresPresentacion IEntrenadoresPresentacion,
            IJuegosPresentacion IJuegosPresentacion,
            IEmpresasPresentacion IEmpresasPresentacion,
            IAcademiasPresentacion IAcademiasPresentacion,
            IEventosPresentacion IEventosPresentacion)
        {
            this.ILoginsPresentacion = ILoginsPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
            this.IJugadoresPresentacion = IJugadoresPresentacion;
            this.IEntrenadoresPresentacion = IEntrenadoresPresentacion;
            this.IJuegosPresentacion = IJuegosPresentacion;
            this.IEmpresasPresentacion = IEmpresasPresentacion;
            this.IAcademiasPresentacion = IAcademiasPresentacion;
            this.IEventosPresentacion = IEventosPresentacion;
        }

        public bool EstaLogueado = false;
        public string? NombreUsuario;
        public string? RolUsuario;
        [BindProperty] public string? Correo { get; set; }
        [BindProperty] public string? Contrasena { get; set; }

        // Datos del panel de inicio
        public List<Total> Totales { get; set; } = new List<Total>();
        public List<Eventos> ProximosEventos { get; set; } = new List<Eventos>();
        public List<Equipos> EquiposPanel { get; set; } = new List<Equipos>();

        public class Total
        {
            public string Etiqueta { get; set; } = "";
            public int Cantidad { get; set; }
        }

        public void OnGet()
        {
            CargarSesion();
            CargarPanel();
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

        // Totales, próximos eventos y equipos para el panel (solo con sesión iniciada)
        private void CargarPanel()
        {
            if (!EstaLogueado)
                return;

            try
            {
                var equipos = this.IEquiposPresentacion.Consultar();
                var eventos = this.IEventosPresentacion.Consultar();

                this.Totales = new List<Total>()
                {
                    new Total() { Etiqueta = "Equipos", Cantidad = equipos.Count },
                    new Total() { Etiqueta = "Jugadores", Cantidad = this.IJugadoresPresentacion.Consultar().Count },
                    new Total() { Etiqueta = "Entrenadores", Cantidad = this.IEntrenadoresPresentacion.Consultar().Count },
                    new Total() { Etiqueta = "Juegos", Cantidad = this.IJuegosPresentacion.Consultar().Count },
                    new Total() { Etiqueta = "Empresas", Cantidad = this.IEmpresasPresentacion.Consultar().Count },
                    new Total() { Etiqueta = "Academias", Cantidad = this.IAcademiasPresentacion.Consultar().Count },
                    new Total() { Etiqueta = "Eventos", Cantidad = eventos.Count }
                };

                this.ProximosEventos = eventos
                    .Where(x => x.FechaFin >= DateTime.Now)
                    .OrderBy(x => x.FechaIni)
                    .Take(5)
                    .ToList();

                this.EquiposPanel = equipos.Take(8).ToList();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
