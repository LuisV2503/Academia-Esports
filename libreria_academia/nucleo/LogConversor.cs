namespace libreria_academia.nucleo
{
    public class LogConversor
    {
        public static void Log(Exception ex,
            IDictionary<string, object?> ViewData)
        {
            if (ViewData == null)
                return;

            var mensaje = ex.Message;
            if (mensaje.Length >= 100)
                mensaje = mensaje.Substring(0, 100);

            ViewData["Mensaje"] = mensaje;
        }
    }
}