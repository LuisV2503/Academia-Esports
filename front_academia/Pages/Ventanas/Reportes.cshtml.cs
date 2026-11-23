using front_academia.Servicios;
using libreria_academia.nucleo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_academia.Pages.Ventanas
{
    // Al abrir la página se descarga directamente el PDF general; si algo falla, se muestra el mensaje
    public class ReportesModel : PageModel
    {
        private ReportePdf ReportePdf;

        public ReportesModel(ReportePdf ReportePdf)
        {
            this.ReportePdf = ReportePdf;
        }

        public IActionResult OnGet()
        {
            try
            {
                var usuario = HttpContext.Session.GetString("Nombre") ?? "";
                var contenido = this.ReportePdf.Generar(usuario);
                return File(contenido, "application/pdf", $"Academia_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                return Page();
            }
        }
    }
}
