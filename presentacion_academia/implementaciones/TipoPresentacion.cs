using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class TipoPresentacion : ITipoPresentacion
    {
        private IComunicaciones IComunicaciones;

        public TipoPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public Tipo Actualizar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Tipo>(
                "Tipo/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Tipo Borrar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Tipo>(
                "Tipo/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Tipo> Buscar(Tipo entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Tipo>>(
                "Tipo/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Tipo> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Tipo>>(
                "Tipo/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Tipo Insertar(Tipo entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            var task = this.IComunicaciones.Ejecutar<Tipo>(
                "Tipo/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Tipo entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripccion);
        }
    }
}
