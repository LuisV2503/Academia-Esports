using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class EntretenimientosPresentacion : IEntretenimientosPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEventosPresentacion IEventosPresentacion;

        public EntretenimientosPresentacion(IComunicaciones IComunicaciones,
            IEventosPresentacion IEventosPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEventosPresentacion = IEventosPresentacion;
        }

        public Entretenimientos Actualizar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var task = this.IComunicaciones.Ejecutar<Entretenimientos>(
                "Entretenimientos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Entretenimientos Borrar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Entretenimientos>(
                "Entretenimientos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Entretenimientos> Buscar(Entretenimientos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Entretenimientos>>(
                "Entretenimientos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Entretenimientos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Entretenimientos>>(
                "Entretenimientos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Entretenimientos Insertar(Entretenimientos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var task = this.IComunicaciones.Ejecutar<Entretenimientos>(
                "Entretenimientos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Entretenimientos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Duracion == null || entidad.Duracion == 0 ||
                entidad.Evento == null || entidad.Evento == 0;
        }
    }
}
