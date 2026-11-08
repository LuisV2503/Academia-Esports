using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Equipos_EmpresasPresentacion : IEquipos_EmpresasPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEquiposPresentacion IEquiposPresentacion;
        private IEmpresasPresentacion IEmpresasPresentacion;

        public Equipos_EmpresasPresentacion(IComunicaciones IComunicaciones,
            IEquiposPresentacion IEquiposPresentacion,
            IEmpresasPresentacion IEmpresasPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEquiposPresentacion = IEquiposPresentacion;
            this.IEmpresasPresentacion = IEmpresasPresentacion;
        }

        public Equipos_Empresas Actualizar(Equipos_Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            var task = this.IComunicaciones.Ejecutar<Equipos_Empresas>(
                "Equipos_Empresas/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Equipos_Empresas Borrar(Equipos_Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Equipos_Empresas>(
                "Equipos_Empresas/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Empresas> Buscar(Equipos_Empresas entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Empresas>>(
                "Equipos_Empresas/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Equipos_Empresas> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Equipos_Empresas>>(
                "Equipos_Empresas/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Equipos_Empresas Insertar(Equipos_Empresas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEquiposPresentacion.Consultar().Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            var task = this.IComunicaciones.Ejecutar<Equipos_Empresas>(
                "Equipos_Empresas/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Equipos_Empresas entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Empresa == null || entidad.Empresa == 0;
        }
    }
}
