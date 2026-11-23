using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace front_academia.Pages.Ventanas
{
    public class PsicologosModel : PageModel
    {
        private const int LimiteImagenKb = 500;

        private IPsicologosPresentacion IPsicologosPresentacion;
        private IPersonasPresentacion IPersonasPresentacion;
        private IEquiposPresentacion IEquiposPresentacion;

        public PsicologosModel(IPsicologosPresentacion IPsicologosPresentacion,
            IPersonasPresentacion IPersonasPresentacion,
            IEquiposPresentacion IEquiposPresentacion)
        {
            this.IPsicologosPresentacion = IPsicologosPresentacion;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
            this.Filtro = new Psicologos();
            this.Filtro._persona = new Personas();
        }

        [BindProperty] public List<Psicologos> Lista { get; set; } = new List<Psicologos>();
        [BindProperty] public Psicologos? Filtro { get; set; }
        [BindProperty] public Psicologos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public IFormFile? FormFile { get; set; }
        public List<SelectListItem> ListaPersona { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaEquipo { get; set; } = new List<SelectListItem>();

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._persona = this.Filtro!._persona ?? new Personas();
                this.Filtro!._persona.Nombre = this.Filtro!._persona.Nombre ?? "";
                Lista = this.IPsicologosPresentacion.Buscar(this.Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Psicologos();
                CargarListas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                LeerImagen();

                if (this.Actual!.Id == null || this.Actual!.Id == 0)
                    this.Actual = this.IPsicologosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IPsicologosPresentacion.Actualizar(this.Actual!);
            }
            catch (Exception ex)
            {
                // Si falla, el formulario sigue abierto con lo que se había escrito
                Formulario = true;
                CargarListas();
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {
                Formulario = true;
                OnPostBtBuscar();
                Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
                CargarListas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                Borrar = true;
                OnPostBtBuscar();
                Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual!.Id == null || this.Actual!.Id == 0)
                    return;
                this.Actual = this.IPsicologosPresentacion.Borrar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtBuscar();
            }
        }

        // Convierte la imagen elegida a base64 (se guarda como texto en la base de datos)
        private void LeerImagen()
        {
            if (FormFile == null || FormFile.Length == 0)
                return;

            if (FormFile.Length > LimiteImagenKb * 1024)
                throw new Exception($"La imagen no puede superar {LimiteImagenKb} KB");

            using var memoryStream = new MemoryStream();
            FormFile.CopyTo(memoryStream);
            this.Actual!.Imagen = Convert.ToBase64String(memoryStream.ToArray());
        }

        private void CargarListas()
        {
            this.ListaPersona = this.IPersonasPresentacion.Consultar()
                .Select(x => new SelectListItem($"{x.Nombre} (Id {x.Id})", x.Id.ToString()))
                .ToList();
            this.ListaEquipo = this.IEquiposPresentacion.Consultar()
                .Select(x => new SelectListItem($"{x.Nombre} (Id {x.Id})", x.Id.ToString()))
                .ToList();
        }
    }
}
