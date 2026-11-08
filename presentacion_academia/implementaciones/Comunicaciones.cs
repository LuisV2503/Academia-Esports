using Newtonsoft.Json;
using System.Text;
using presentacion_academia.interfaces;

namespace presentacion_academia.implementaciones
{
    public class Comunicaciones : IComunicaciones
    {
        public async Task<T> Ejecutar<T>(string url, object? entidad, HttpMethod metodo)
        {
            url = "http://localhost:5235/" + url;

            var cliente_http = new HttpClient();
            cliente_http.Timeout = new TimeSpan(0, 4, 0);

            var peticion = new HttpRequestMessage(metodo, url);

            // Un GET no lleva cuerpo; POST, PUT y DELETE sí
            if (metodo != HttpMethod.Get)
            {
                peticion.Content = new StringContent(
                    JsonConvert.SerializeObject(entidad),
                    Encoding.UTF8, "application/json");
            }

            var message = await cliente_http.SendAsync(peticion);

            if (!message.IsSuccessStatusCode)
            {
                var error = await message.Content.ReadAsStringAsync();
                throw new Exception(string.IsNullOrWhiteSpace(error)
                    ? "Error Comunicacion"
                    : error);
            }

            var respuesta = await message.Content.ReadAsStringAsync();
            cliente_http.Dispose();
            cliente_http = null;

            respuesta = Replace(respuesta);
            return JsonConvert.DeserializeObject<T>(respuesta)!;
        }

        private string Replace(string resp)
        {
            return resp.Replace("\\\\r\\\\n", "")
                .Replace("\\r\\n", "")
                .Replace("\\", "")
                .Replace("\\\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("\\\\", "\\")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\\n", "")
                .Replace("\\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "")
                .Replace("null", "''");
        }
    }
}