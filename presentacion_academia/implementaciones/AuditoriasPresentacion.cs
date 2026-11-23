using libreria_academia.entidades;
using presentacion_academia.interfaces;

namespace aplicacion_academia.interfaces
{
    public class AuditoriasPresentacion : IAuditoriasPresentacion
    {
        private IComunicaciones IComunicaciones;

        public AuditoriasPresentacion(IComunicaciones IComunicaciones)
        {
            this.IComunicaciones = IComunicaciones;
        }

        public List<Auditorias> Buscar(Auditorias entidad)
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditorias>>(
                "Auditorias/Buscar",
                entidad,
                HttpMethod.Post);
            task.Wait();
            return task.Result;
        }

        public List<Auditorias> Consultar()
        {
            var task = this.IComunicaciones.Ejecutar<List<Auditorias>>(
                "Auditorias/Consultar",
                null,
                HttpMethod.Get);
            task.Wait();
            return task.Result;
        }
    }
}
