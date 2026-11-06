using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class EventosPresentacion : IEventosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public EventosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Eventos Actualizar(Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (entidad.FechaFin < entidad.FechaIni)
                throw new Exception("Fechas invalidas");

            var task = this.IComunicaciones.Ejecutar<Eventos>(
                "Eventos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Eventos Borrar(Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Eventos>(
                "Eventos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Eventos> Buscar(Eventos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Eventos>>(
                "Eventos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Eventos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Eventos>>(
                "Eventos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Eventos Insertar(Eventos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (entidad.FechaFin < entidad.FechaIni)
                throw new Exception("Fechas invalidas");

            var task = this.IComunicaciones.Ejecutar<Eventos>(
                "Eventos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Eventos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.FechaIni == null ||
                entidad.FechaFin == null ||
                string.IsNullOrEmpty(entidad.Modalidad) ||
                string.IsNullOrEmpty(entidad.Ciudad) ||
                string.IsNullOrEmpty(entidad.Direccion);
        }
    }
}
