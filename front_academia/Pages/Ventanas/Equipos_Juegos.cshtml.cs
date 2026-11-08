using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Equipos_JuegosModel : PageModel
    {
        private IEquipos_JuegosPresentacion IEquipos_JuegosPresentacion;

        public Equipos_JuegosModel(IEquipos_JuegosPresentacion IEquipos_JuegosPresentacion)
        {
            this.IEquipos_JuegosPresentacion = IEquipos_JuegosPresentacion;
            this.Filtro = new Equipos_Juegos();
            this.Filtro._equipos = new Equipos();
        }

        [BindProperty] public List<Equipos_Juegos> Lista { get; set; } = new List<Equipos_Juegos>();
        [BindProperty] public Equipos_Juegos? Filtro { get; set; }
        [BindProperty] public Equipos_Juegos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._equipos = this.Filtro!._equipos ?? new Equipos();
                this.Filtro!._equipos.Nombre = this.Filtro!._equipos.Nombre ?? "";
                Lista = this.IEquipos_JuegosPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Equipos_Juegos();
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
                    this.Actual = this.IEquipos_JuegosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEquipos_JuegosPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEquipos_JuegosPresentacion.Borrar(this.Actual!);
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
