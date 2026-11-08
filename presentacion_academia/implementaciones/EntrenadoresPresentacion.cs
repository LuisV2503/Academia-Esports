using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class EntrenadoresPresentacion : IEntrenadoresPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IPersonasPresentacion IPersonasPresentacion;
        private IEquiposPresentacion IEquiposPresentacion;

        public EntrenadoresPresentacion(IComunicaciones IComunicaciones,
            IPersonasPresentacion IPersonasPresentacion,
            IEquiposPresentacion IEquiposPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
        }

        public Entrenadores Actualizar(Entrenadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Entrenadores>(
                "Entrenadores/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Entrenadores Borrar(Entrenadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Entrenadores>(
                "Entrenadores/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Entrenadores> Buscar(Entrenadores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Entrenadores>>(
                "Entrenadores/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Entrenadores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Entrenadores>>(
                "Entrenadores/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Entrenadores Insertar(Entrenadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Entrenadores>(
                "Entrenadores/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Entrenadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
