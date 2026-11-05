using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class SueldosPresentacion : ISueldosPresentacion
    {
        private IComunicaciones IComunicaciones;

        public SueldosPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Sueldos Actualizar(Sueldos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Sueldos>(
                "Sueldos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Sueldos Borrar(Sueldos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var personas = this.IComunicaciones.Ejecutar<List<Personas>>(
                "Personas/Consultar",
                null,
                HttpMethod.Get);
            personas.Wait();

            if (personas.Result.Any(x => x.Sueldo == entidad.Id))
                throw new Exception("Sueldo en uso");

            var task = this.IComunicaciones.Ejecutar<Sueldos>(
                "Sueldos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Sueldos> Buscar(Sueldos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Sueldos>>(
                "Sueldos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Sueldos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Sueldos>>(
                "Sueldos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Sueldos Insertar(Sueldos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Sueldos>(
                "Sueldos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Sueldos entidad)
        {
            return
                entidad.Valor == null;
        }
    }
}