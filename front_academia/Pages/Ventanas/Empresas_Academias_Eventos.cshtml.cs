using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Empresas_Academias_EventosModel : PageModel
    {
        private IEmpresas_Academias_EventosPresentacion IEmpresas_Academias_EventosPresentacion;

        public Empresas_Academias_EventosModel(IEmpresas_Academias_EventosPresentacion IEmpresas_Academias_EventosPresentacion)
        {
            this.IEmpresas_Academias_EventosPresentacion = IEmpresas_Academias_EventosPresentacion;
            this.Filtro = new Empresas_Academias_Eventos();
            this.Filtro._eventos = new Eventos();
        }

        [BindProperty] public List<Empresas_Academias_Eventos> Lista { get; set; } = new List<Empresas_Academias_Eventos>();
        [BindProperty] public Empresas_Academias_Eventos? Filtro { get; set; }
        [BindProperty] public Empresas_Academias_Eventos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._eventos = this.Filtro!._eventos ?? new Eventos();
                this.Filtro!._eventos.Nombre = this.Filtro!._eventos.Nombre ?? "";
                Lista = this.IEmpresas_Academias_EventosPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Empresas_Academias_Eventos();
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
                    this.Actual = this.IEmpresas_Academias_EventosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEmpresas_Academias_EventosPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEmpresas_Academias_EventosPresentacion.Borrar(this.Actual!);
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
