using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EntrenadoresAplicacion : IEntrenadoresAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public EntrenadoresAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Entrenadores Actualizar(Entrenadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Entrenadores!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Entrenadores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el entrenador Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        public Entrenadores Borrar(Entrenadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Entrenadores!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Entrenadores!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el entrenador Id {anterior.Id} (Persona {anterior.Persona}, Equipo {anterior.Equipo})");
            return entidad;
        }

        public List<Entrenadores> Buscar(Entrenadores entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Entrenadores!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Entrenadores> Consultar()
        {
            return this.conexion.Entrenadores!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();
        }

        public Entrenadores Insertar(Entrenadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            this.conexion.Entrenadores!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el entrenador Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        private bool Validar(Entrenadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
