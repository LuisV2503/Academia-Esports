
using libreria_academia.entidades;
using presentacion_academia.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    public class PersonasModel : PageModel
    {
        private IPersonasPresentacion IPersonasPresentacion;

        public PersonasModel(IPersonasPresentacion IPersonasPresentacion)
        {
            this.IPersonasPresentacion = IPersonasPresentacion;
        }

        [BindProperty] public List<Personas> Lista { get; set; }

        public void OnGet() { OnPostBtRefrescar(); }

        public void OnPostBtRefrescar()
        {
            try
            {
                Lista = this.IPersonasPresentacion.Consultar();
            }
            catch (Exception ex)
            {

            }
        }
    }
}