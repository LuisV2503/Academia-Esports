using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class Equipos_EmpresasModel : PageModel
    {
        private IEquipos_EmpresasPresentacion IEquipos_EmpresasPresentacion;

        public Equipos_EmpresasModel(IEquipos_EmpresasPresentacion IEquipos_EmpresasPresentacion)
        {
            this.IEquipos_EmpresasPresentacion = IEquipos_EmpresasPresentacion;
            this.Filtro = new Equipos_Empresas();
            this.Filtro._equipos = new Equipos();
        }

        [BindProperty] public List<Equipos_Empresas> Lista { get; set; } = new List<Equipos_Empresas>();
        [BindProperty] public Equipos_Empresas? Filtro { get; set; }
        [BindProperty] public Equipos_Empresas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!._equipos = this.Filtro!._equipos ?? new Equipos();
                this.Filtro!._equipos.Nombre = this.Filtro!._equipos.Nombre ?? "";
                Lista = this.IEquipos_EmpresasPresentacion.Buscar(this.Filtro!);
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
                this.Actual = new Equipos_Empresas();
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
                    this.Actual = this.IEquipos_EmpresasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEquipos_EmpresasPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IEquipos_EmpresasPresentacion.Borrar(this.Actual!);
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
