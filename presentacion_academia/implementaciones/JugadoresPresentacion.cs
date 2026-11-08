using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class JugadoresPresentacion : IJugadoresPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IPersonasPresentacion IPersonasPresentacion;
        private IEquiposPresentacion IEquiposPresentacion;

        public JugadoresPresentacion(IComunicaciones IComunicaciones,
            IPersonasPresentacion IPersonasPresentacion,
            IEquiposPresentacion IEquiposPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
        }

        public Jugadores Actualizar(Jugadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Jugadores>(
                "Jugadores/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Jugadores Borrar(Jugadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Jugadores>(
                "Jugadores/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Jugadores> Buscar(Jugadores entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Jugadores>>(
                "Jugadores/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Jugadores> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Jugadores>>(
                "Jugadores/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Jugadores Insertar(Jugadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Jugadores>(
                "Jugadores/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Jugadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
