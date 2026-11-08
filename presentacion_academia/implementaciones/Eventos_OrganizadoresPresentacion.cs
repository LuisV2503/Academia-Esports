using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Eventos_OrganizadoresPresentacion : IEventos_OrganizadoresPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEventosPresentacion IEventosPresentacion;
        private IOrganizadoresPresentacion IOrganizadoresPresentacion;

        public Eventos_OrganizadoresPresentacion(IComunicaciones IComunicaciones,
            IEventosPresentacion IEventosPresentacion,
            IOrganizadoresPresentacion IOrganizadoresPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEventosPresentacion = IEventosPresentacion;
            this.IOrganizadoresPresentacion = IOrganizadoresPresentacion;
        }

        public Eventos_Organizadores Actualizar(Eventos_Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (!this.IOrganizadoresPresentacion.Consultar().Any(x => x.Id == entidad.Organizador))
                throw new Exception("Organizador invalido");

            var task = this.IComunicaciones.Ejecutar<Eventos_Organizadores>(
                "Eventos_Organizadores/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Eventos_Organizadores Borrar(Eventos_Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Eventos_Organizadores>(
                "Eventos_Organizadores/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Eventos_Organizadores> Buscar(Eventos_Organizadores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Eventos_Organizadores>>(
                "Eventos_Organizadores/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Eventos_Organizadores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Eventos_Organizadores>>(
                "Eventos_Organizadores/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Eventos_Organizadores Insertar(Eventos_Organizadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (!this.IOrganizadoresPresentacion.Consultar().Any(x => x.Id == entidad.Organizador))
                throw new Exception("Organizador invalido");

            var task = this.IComunicaciones.Ejecutar<Eventos_Organizadores>(
                "Eventos_Organizadores/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Eventos_Organizadores entidad)
        {
            return
                entidad.Evento == null || entidad.Evento == 0 ||
                entidad.Organizador == null || entidad.Organizador == 0;
        }
    }
}
