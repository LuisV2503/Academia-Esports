using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class EmpresasPresentacion : IEmpresasPresentacion
    {
        private IComunicaciones IComunicaciones;
        private ITipoPresentacion ITipoPresentacion;

        public EmpresasPresentacion(IComunicaciones IComunicaciones,
            ITipoPresentacion ITipoPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.ITipoPresentacion = ITipoPresentacion;
        }

        public Empresas Actualizar(Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.ITipoPresentacion.Consultar().Any(x => x.Id == entidad.Tipo))
                throw new Exception("Tipo invalido");

            var task = this.IComunicaciones.Ejecutar<Empresas>(
                "Empresas/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Empresas Borrar(Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Empresas>(
                "Empresas/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Empresas> Buscar(Empresas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas>>(
                "Empresas/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Empresas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas>>(
                "Empresas/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Empresas Insertar(Empresas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.ITipoPresentacion.Consultar().Any(x => x.Id == entidad.Tipo))
                throw new Exception("Tipo invalido");

            var task = this.IComunicaciones.Ejecutar<Empresas>(
                "Empresas/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Empresas entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Pais) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                entidad.Tipo == null || entidad.Tipo == 0;
        }
    }
}
