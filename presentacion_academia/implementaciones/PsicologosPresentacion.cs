using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class PsicologosPresentacion : IPsicologosPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IPersonasPresentacion IPersonasPresentacion;
        private IEquiposPresentacion IEquiposPresentacion;

        public PsicologosPresentacion(IComunicaciones IComunicaciones,
            IPersonasPresentacion IPersonasPresentacion,
            IEquiposPresentacion IEquiposPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IPersonasPresentacion = IPersonasPresentacion;
            this.IEquiposPresentacion = IEquiposPresentacion;
        }

        public Psicologos Actualizar(Psicologos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Psicologos>(
                "Psicologos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Psicologos Borrar(Psicologos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Psicologos>(
                "Psicologos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Psicologos> Buscar(Psicologos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Psicologos>>(
                "Psicologos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Psicologos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Psicologos>>(
                "Psicologos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Psicologos Insertar(Psicologos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IPersonasPresentacion.Consultar().Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            var task = this.IComunicaciones.Ejecutar<Psicologos>(
                "Psicologos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Psicologos entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
