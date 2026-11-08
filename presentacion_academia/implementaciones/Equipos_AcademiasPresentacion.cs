using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Equipos_AcademiasPresentacion : IEquipos_AcademiasPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEquiposPresentacion IEquiposPresentacion;
        private IAcademiasPresentacion IAcademiasPresentacion;

        public Equipos_AcademiasPresentacion(IComunicaciones IComunicaciones,
            IEquiposPresentacion IEquiposPresentacion,
            IAcademiasPresentacion IAcademiasPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEquiposPresentacion = IEquiposPresentacion;
            this.IAcademiasPresentacion = IAcademiasPresentacion;
        }

        public Equipos_Academias Actualizar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            var task = this.IComunicaciones.Ejecutar<Equipos_Academias>(
                "Equipos_Academias/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Equipos_Academias Borrar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Equipos_Academias>(
                "Equipos_Academias/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Academias> Buscar(Equipos_Academias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Academias>>(
                "Equipos_Academias/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Academias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Academias>>(
                "Equipos_Academias/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Equipos_Academias Insertar(Equipos_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            var task = this.IComunicaciones.Ejecutar<Equipos_Academias>(
                "Equipos_Academias/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Equipos_Academias entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Academia == null || entidad.Academia == 0;
        }
    }
}
