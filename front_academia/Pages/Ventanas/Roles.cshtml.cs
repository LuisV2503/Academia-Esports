using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace front_academia.Pages.Ventanas
{
    public class RolesModel : PageModel
    {
        private const int LimiteImagenKb = 500;

        private IRolesPresentacion IRolesPresentacion;

        public RolesModel(IRolesPresentacion IRolesPresentacion)
        {
            this.IRolesPresentacion = IRolesPresentacion;
            this.Filtro = new Roles();
        }

        [BindProperty] public List<Roles> Lista { get; set; } = new List<Roles>();
        [BindProperty] public Roles? Filtro { get; set; }
        [BindProperty] public Roles? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        [BindProperty] public IFormFile? FormFile { get; set; }

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!.Nombre = this.Filtro!.Nombre ?? "";
                Lista = this.IRolesPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Roles();
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
                    this.Actual = this.IRolesPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IRolesPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IRolesPresentacion.Borrar(this.Actual!);
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
        }
    }
}
