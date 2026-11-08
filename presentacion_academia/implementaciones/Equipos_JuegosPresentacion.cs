using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Equipos_JuegosPresentacion : IEquipos_JuegosPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEquiposPresentacion IEquiposPresentacion;
        private IJuegosPresentacion IJuegosPresentacion;

        public Equipos_JuegosPresentacion(IComunicaciones IComunicaciones,
            IEquiposPresentacion IEquiposPresentacion,
            IJuegosPresentacion IJuegosPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEquiposPresentacion = IEquiposPresentacion;
            this.IJuegosPresentacion = IJuegosPresentacion;
        }

        public Equipos_Juegos Actualizar(Equipos_Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IJuegosPresentacion.Consultar().Any(x => x.Id == entidad.Juego))
                throw new Exception("Juego invalido");

            var task = this.IComunicaciones.Ejecutar<Equipos_Juegos>(
                "Equipos_Juegos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Equipos_Juegos Borrar(Equipos_Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Equipos_Juegos>(
                "Equipos_Juegos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Juegos> Buscar(Equipos_Juegos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Juegos>>(
                "Equipos_Juegos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Juegos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Juegos>>(
                "Equipos_Juegos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Equipos_Juegos Insertar(Equipos_Juegos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IJuegosPresentacion.Consultar().Any(x => x.Id == entidad.Juego))
                throw new Exception("Juego invalido");

            var task = this.IComunicaciones.Ejecutar<Equipos_Juegos>(
                "Equipos_Juegos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Equipos_Juegos entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Juego == null || entidad.Juego == 0;
        }
    }
}
