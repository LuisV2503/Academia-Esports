using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class EntrenadoresModel : PageModel
    {
        private IEntrenadoresPresentacion IEntrenadoresPresentacion;

        public EntrenadoresModel(IEntrenadoresPresentacion IEntrenadoresPresentacion)
        {
            this.IEntrenadoresPresentacion = IEntrenadoresPresentacion;
            this.Filtro = new Entrenadores();
            this.Filtro._persona = new Personas();
        }

        [BindProperty] public List<Entrenadores> Lista { get; set; } = new List<Entrenadores>();
        [BindProperty] public Entrenadores? Filtro { get; set; }
        [BindProperty] public Entrenadores? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._persona = this.Filtro!._persona ?? new Personas();
                this.Filtro!._persona.Nombre = this.Filtro!._persona.Nombre ?? "";
                Lista = this.IEntrenadoresPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Entrenadores();
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
                if (this.Actual!.Id == null || this.Actual!.Id == 0)
                    this.Actual = this.IEntrenadoresPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEntrenadoresPresentacion.Actualizar(this.Actual!);
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

        public void OnPostBtEditar(string data)
        {
            try
            {
                Formulario = true;
                OnPostBtBuscar();
                Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                this.Actual = this.IEntrenadoresPresentacion.Borrar(this.Actual!);
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
    }
}
