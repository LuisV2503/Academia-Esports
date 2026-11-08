using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Eventos_OrganizadoresAplicacion : IEventos_OrganizadoresAplicacion
    {
        private IConexion conexion;

        public Eventos_OrganizadoresAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Eventos_Organizadores Actualizar(Eventos_Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (!this.conexion.Organizadores!.Any(x => x.Id == entidad.Organizador))
                throw new Exception("Organizador invalido");

            if (this.conexion.Eventos_Organizadores!.Any(x => x.Id != entidad.Id &&
                x.Evento == entidad.Evento &&
                x.Organizador == entidad.Organizador))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Eventos_Organizadores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Eventos_Organizadores Borrar(Eventos_Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Eventos_Organizadores!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Eventos_Organizadores> Buscar(Eventos_Organizadores entidad)
        {
            var nombre = entidad._eventos?.Nombre ?? "";

            return this.conexion.Eventos_Organizadores!
                .Include(x => x._eventos)
                .Include(x => x._organizadores)
                .ThenInclude(x => x._persona)
                .Where(x => x._eventos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Eventos_Organizadores> Consultar()
        {
            return this.conexion.Eventos_Organizadores!
                .Include(x => x._eventos)
                .Include(x => x._organizadores)
                .ThenInclude(x => x._persona)
                .ToList();
        }

        public Eventos_Organizadores Insertar(Eventos_Organizadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (!this.conexion.Organizadores!.Any(x => x.Id == entidad.Organizador))
                throw new Exception("Organizador invalido");

            if (this.conexion.Eventos_Organizadores!.Any(x => x.Id != entidad.Id &&
                x.Evento == entidad.Evento &&
                x.Organizador == entidad.Organizador))
                throw new Exception("Relacion repetida");

            this.conexion.Eventos_Organizadores!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Eventos_Organizadores entidad)
        {
            return
                entidad.Evento == null || entidad.Evento == 0 ||
                entidad.Organizador == null || entidad.Organizador == 0;
        }
    }
}
