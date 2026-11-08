using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class OrganizadoresPresentacion : IOrganizadoresPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IPersonasPresentacion IPersonasPresentacion;

        public OrganizadoresPresentacion(IComunicaciones IComunicaciones,
            IPersonasPresentacion IPersonasPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IPersonasPresentacion = IPersonasPresentacion;
        }

        public Organizadores Actualizar(Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            var task = this.IComunicaciones.Ejecutar<Organizadores>(
                "Organizadores/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Organizadores Borrar(Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Organizadores>(
                "Organizadores/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Organizadores> Buscar(Organizadores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Organizadores>>(
                "Organizadores/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Organizadores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Organizadores>>(
                "Organizadores/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Organizadores Insertar(Organizadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            var task = this.IComunicaciones.Ejecutar<Organizadores>(
                "Organizadores/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Organizadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0;
        }
    }
}
