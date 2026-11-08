using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class OrganizadoresAplicacion : IOrganizadoresAplicacion
    {
        private IConexion conexion;

        public OrganizadoresAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Organizadores Actualizar(Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Organizadores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Organizadores Borrar(Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Eventos_Organizadores!.Any(x => x.Organizador == entidad.Id))
                throw new Exception("Organizador en uso");

            this.conexion.Organizadores!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Organizadores> Buscar(Organizadores entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Organizadores!
                .Include(x => x._persona)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Organizadores> Consultar()
        {
            return this.conexion.Organizadores!
                .Include(x => x._persona)
                .ToList();
        }

        public Organizadores Insertar(Organizadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (entidad.Activo == null)
                entidad.Activo = false;

            this.conexion.Organizadores!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Organizadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0;
        }
    }
}
