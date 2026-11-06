using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EntretenimientosAplicacion : IEntretenimientosAplicacion
    {
        private IConexion conexion;

        public EntretenimientosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Entretenimientos Actualizar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            var entry = this.conexion!.Entry<Entretenimientos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Entretenimientos Borrar(Entretenimientos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Entretenimientos!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Entretenimientos> Buscar(Entretenimientos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Entretenimientos!
                .Include(x => x._eventos)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Entretenimientos> Consultar()
        {
            return this.conexion.Entretenimientos!
                .Include(x => x._eventos)
                .ToList();
        }

        public Entretenimientos Insertar(Entretenimientos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            this.conexion.Entretenimientos!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Entretenimientos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                entidad.Duracion == null || entidad.Duracion == 0 ||
                entidad.Evento == null || entidad.Evento == 0;
        }
    }
}
