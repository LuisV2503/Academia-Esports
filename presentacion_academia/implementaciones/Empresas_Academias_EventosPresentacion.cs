using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class Empresas_Academias_EventosPresentacion : IEmpresas_Academias_EventosPresentacion
    {
        private IComunicaciones IComunicaciones;
        private IEmpresasPresentacion IEmpresasPresentacion;
        private IAcademiasPresentacion IAcademiasPresentacion;
        private IEventosPresentacion IEventosPresentacion;

        public Empresas_Academias_EventosPresentacion(IComunicaciones IComunicaciones,
            IEmpresasPresentacion IEmpresasPresentacion,
            IAcademiasPresentacion IAcademiasPresentacion,
            IEventosPresentacion IEventosPresentacion)
        {
            this.IComunicaciones = IComunicaciones;
            this.IEmpresasPresentacion = IEmpresasPresentacion;
            this.IAcademiasPresentacion = IAcademiasPresentacion;
            this.IEventosPresentacion = IEventosPresentacion;
        }

        public Empresas_Academias_Eventos Actualizar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias_Eventos>(
                "Empresas_Academias_Eventos/Actualizar",
                entidad,
                HttpMethod.Put);
            task.Wait();
            return task.Result;
        }

        public Empresas_Academias_Eventos Borrar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se ha guardado");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias_Eventos>(
                "Empresas_Academias_Eventos/Borrar",
                entidad,
                HttpMethod.Delete);
            task.Wait();
            return task.Result;
        }

        public List<Empresas_Academias_Eventos> Buscar(Empresas_Academias_Eventos entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas_Academias_Eventos>>(
                "Empresas_Academias_Eventos/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Empresas_Academias_Eventos> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Empresas_Academias_Eventos>>(
                "Empresas_Academias_Eventos/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }

        public Empresas_Academias_Eventos Insertar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Falta informacion");

            if (!this.IEmpresasPresentacion.Consultar().Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.IAcademiasPresentacion.Consultar().Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (!this.IEventosPresentacion.Consultar().Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var task = this.IComunicaciones.Ejecutar<Empresas_Academias_Eventos>(
                "Empresas_Academias_Eventos/Insertar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        private bool Validar(Empresas_Academias_Eventos entidad)
        {
            return
                entidad.Empresa == null || entidad.Empresa == 0 ||
                entidad.Academia == null || entidad.Academia == 0 ||
                entidad.Evento == null || entidad.Evento == 0;
        }
    }
}
