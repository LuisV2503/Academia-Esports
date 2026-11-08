using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Empresas_AcademiasModel : PageModel
    {
        private IEmpresas_AcademiasPresentacion IEmpresas_AcademiasPresentacion;

        public Empresas_AcademiasModel(IEmpresas_AcademiasPresentacion IEmpresas_AcademiasPresentacion)
        {
            this.IEmpresas_AcademiasPresentacion = IEmpresas_AcademiasPresentacion;
            this.Filtro = new Empresas_Academias();
            this.Filtro._empresas = new Empresas();
        }

        [BindProperty] public List<Empresas_Academias> Lista { get; set; } = new List<Empresas_Academias>();
        [BindProperty] public Empresas_Academias? Filtro { get; set; }
        [BindProperty] public Empresas_Academias? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

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
                    this.Actual = this.IEmpresas_AcademiasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEmpresas_AcademiasPresentacion.Actualizar(this.Actual!);
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
    }
}
