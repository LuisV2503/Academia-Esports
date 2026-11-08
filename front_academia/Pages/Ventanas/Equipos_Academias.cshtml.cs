using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Equipos_AcademiasModel : PageModel
    {
        private IEquipos_AcademiasPresentacion IEquipos_AcademiasPresentacion;

        public Equipos_AcademiasModel(IEquipos_AcademiasPresentacion IEquipos_AcademiasPresentacion)
        {
            this.IEquipos_AcademiasPresentacion = IEquipos_AcademiasPresentacion;
            this.Filtro = new Equipos_Academias();
            this.Filtro._equipos = new Equipos();
        }

        [BindProperty] public List<Equipos_Academias> Lista { get; set; } = new List<Equipos_Academias>();
        [BindProperty] public Equipos_Academias? Filtro { get; set; }
        [BindProperty] public Equipos_Academias? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._equipos = this.Filtro!._equipos ?? new Equipos();
                this.Filtro!._equipos.Nombre = this.Filtro!._equipos.Nombre ?? "";
                Lista = this.IEquipos_AcademiasPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Equipos_Academias();
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
                    this.Actual = this.IEquipos_AcademiasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEquipos_AcademiasPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEquipos_AcademiasPresentacion.Borrar(this.Actual!);
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
