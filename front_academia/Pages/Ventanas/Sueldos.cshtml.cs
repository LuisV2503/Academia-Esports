
using libreria_academia.entidades;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class SueldosModel : PageModel
    {
        private ISueldosPresentacion ISueldosPresentacion;

        public SueldosModel(ISueldosPresentacion ISueldosPresentacion)
        {
            this.ISueldosPresentacion = ISueldosPresentacion;
        }

        [BindProperty] public List<Sueldos> Lista { get; set; }

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = this.ISueldosPresentacion.Consultar();
            }
            catch (Exception ex)
            {

            }
        }
    }
}