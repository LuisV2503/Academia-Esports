using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Eventos_OrganizadoresModel : PageModel
    {
        private IEventos_OrganizadoresPresentacion IEventos_OrganizadoresPresentacion;

        public Eventos_OrganizadoresModel(IEventos_OrganizadoresPresentacion IEventos_OrganizadoresPresentacion)
        {
            this.IEventos_OrganizadoresPresentacion = IEventos_OrganizadoresPresentacion;
            this.Filtro = new Eventos_Organizadores();
            this.Filtro._eventos = new Eventos();
        }

        [BindProperty] public List<Eventos_Organizadores> Lista { get; set; } = new List<Eventos_Organizadores>();
        [BindProperty] public Eventos_Organizadores? Filtro { get; set; }
        [BindProperty] public Eventos_Organizadores? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._eventos = this.Filtro!._eventos ?? new Eventos();
                this.Filtro!._eventos.Nombre = this.Filtro!._eventos.Nombre ?? "";
                Lista = this.IEventos_OrganizadoresPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Eventos_Organizadores();
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
                    this.Actual = this.IEventos_OrganizadoresPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEventos_OrganizadoresPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEventos_OrganizadoresPresentacion.Borrar(this.Actual!);
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
