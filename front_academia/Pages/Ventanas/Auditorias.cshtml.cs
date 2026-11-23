using libreria_academia.entidades;
using libreria_academia.nucleo;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    // Solo consulta: las auditorías no se crean, editan ni borran desde la ventana
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasPresentacion IAuditoriasPresentacion;

        public AuditoriasModel(IAuditoriasPresentacion IAuditoriasPresentacion)
        {
            this.IAuditoriasPresentacion = IAuditoriasPresentacion;
            this.Filtro = new Auditorias();
        }

        [BindProperty] public List<Auditorias> Lista { get; set; } = new List<Auditorias>();
        [BindProperty] public Auditorias? Filtro { get; set; }

        public void OnGet() { OnPostBtBuscar(); }

        public void OnPostBtBuscar()
        {
            try
            {
                this.Filtro!.Descripcion = this.Filtro!.Descripcion ?? "";
                Lista = this.IAuditoriasPresentacion.Buscar(this.Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
