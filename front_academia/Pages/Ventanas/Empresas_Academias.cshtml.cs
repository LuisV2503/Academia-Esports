using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace front_academia.Pages.Ventanas
{
    public class Empresas_AcademiasModel : PageModel
    {
        private const int LimiteImagenKb = 500;

        private IEmpresas_AcademiasPresentacion IEmpresas_AcademiasPresentacion;
        private IEmpresasPresentacion IEmpresasPresentacion;
        private IAcademiasPresentacion IAcademiasPresentacion;

        public Empresas_AcademiasModel(IEmpresas_AcademiasPresentacion IEmpresas_AcademiasPresentacion,
            IEmpresasPresentacion IEmpresasPresentacion,
            IAcademiasPresentacion IAcademiasPresentacion)
        {
            this.IEmpresas_AcademiasPresentacion = IEmpresas_AcademiasPresentacion;
            this.IEmpresasPresentacion = IEmpresasPresentacion;
            this.IAcademiasPresentacion = IAcademiasPresentacion;
            this.Filtro = new Empresas_Academias();
            this.Filtro._empresas = new Empresas();
        }

        [BindProperty] public List<Empresas_Academias> Lista { get; set; } = new List<Empresas_Academias>();
        [BindProperty] public Empresas_Academias? Filtro { get; set; }
        [BindProperty] public Empresas_Academias? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public IFormFile? FormFile { get; set; }
        public List<SelectListItem> ListaEmpresa { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ListaAcademia { get; set; } = new List<SelectListItem>();

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._empresas = this.Filtro!._empresas ?? new Empresas();
                this.Filtro!._empresas.Nombre = this.Filtro!._empresas.Nombre ?? "";
                Lista = this.IEmpresas_AcademiasPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Empresas_Academias();
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
                    this.Actual = this.IEmpresas_AcademiasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEmpresas_AcademiasPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEmpresas_AcademiasPresentacion.Borrar(this.Actual!);
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
            this.ListaEmpresa = this.IEmpresasPresentacion.Consultar()
                .Select(x => new SelectListItem($"{x.Nombre} (Id {x.Id})", x.Id.ToString()))
                .ToList();
            this.ListaAcademia = this.IAcademiasPresentacion.Consultar()
                .Select(x => new SelectListItem($"{x.Nombre} (Id {x.Id})", x.Id.ToString()))
                .ToList();
        }
    }
}
