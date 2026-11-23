using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class AnalistasPresentacion : IAnalistasPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IPersonasPresentacion IPersonasPresentacion;
        private IEquiposPresentacion IEquiposPresentacion;

        public AnalistasPresentacion(IComunicaciones IComunicaciones,
            IPersonasPresentacion IPersonasPresentacion,
            IEquiposPresentacion IEquiposPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
        }

        public Analistas Actualizar(Analistas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Analistas>(
                "Analistas/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Analistas Borrar(Analistas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Analistas>(
                "Analistas/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Analistas> Buscar(Analistas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Analistas>>(
                "Analistas/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Analistas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Analistas>>(
                "Analistas/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Analistas Insertar(Analistas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Analistas>(
                "Analistas/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Analistas entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
