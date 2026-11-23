using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Empresas_AcademiasPresentacion : IEmpresas_AcademiasPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEmpresasPresentacion IEmpresasPresentacion;
        private IAcademiasPresentacion IAcademiasPresentacion;

        public Empresas_AcademiasPresentacion(IComunicaciones IComunicaciones,
            IEmpresasPresentacion IEmpresasPresentacion,
            IAcademiasPresentacion IAcademiasPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEmpresasPresentacion = IEmpresasPresentacion;
            this.IAcademiasPresentacion = IAcademiasPresentacion;
        }

        public Empresas_Academias Actualizar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias>(
                "Empresas_Academias/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Empresas_Academias Borrar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias>(
                "Empresas_Academias/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Empresas_Academias> Buscar(Empresas_Academias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas_Academias>>(
                "Empresas_Academias/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Empresas_Academias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas_Academias>>(
                "Empresas_Academias/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Empresas_Academias Insertar(Empresas_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias>(
                "Empresas_Academias/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Empresas_Academias entidad)
        {
            return
                entidad.Empresa == null || entidad.Empresa == 0 ||
                entidad.Academia == null || entidad.Academia == 0;
        }
    }
}
