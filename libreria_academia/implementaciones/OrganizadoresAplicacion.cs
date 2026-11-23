using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class OrganizadoresAplicacion : IOrganizadoresAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public OrganizadoresAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
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

            this.auditoria.Registrar($"actualizó el organizador Id {entidad.Id} (Persona {entidad.Persona})");
            return entidad;
        }

        public Organizadores Borrar(Organizadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Eventos_Organizadores!.Any(x => x.Organizador == entidad.Id))
                throw new Exception("Organizador en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Organizadores!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Organizadores!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el organizador Id {anterior.Id} (Persona {anterior.Persona})");
            return entidad;
        }

        public List<Organizadores> Buscar(Organizadores entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            var lista = this.conexion.Organizadores!
                .Include(x => x._persona)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();

            // La foto del rol es la de su persona
            foreach (var elemento in lista)
                elemento.Imagen = elemento._persona?.Imagen;

            return lista;
        }

        public List<Organizadores> Consultar()
        {
            var lista = this.conexion.Organizadores!
                .Include(x => x._persona)
                .ToList();

            // La foto del rol es la de su persona
            foreach (var elemento in lista)
                elemento.Imagen = elemento._persona?.Imagen;

            return lista;
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

            this.auditoria.Registrar($"insertó el organizador Id {entidad.Id} (Persona {entidad.Persona})");
            return entidad;
        }

        private bool Validar(Organizadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0;
        }
    }
}
