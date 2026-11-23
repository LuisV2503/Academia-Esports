using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class AnalistasAplicacion : IAnalistasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public AnalistasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Analistas Actualizar(Analistas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Analistas!
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

            var entry = this.conexion!.Entry<Analistas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el analista Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        public Analistas Borrar(Analistas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Analistas!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Analistas!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el analista Id {anterior.Id} (Persona {anterior.Persona}, Equipo {anterior.Equipo})");
            return entidad;
        }

        public List<Analistas> Buscar(Analistas entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Analistas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Analistas> Consultar()
        {
            return this.conexion.Analistas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();
        }

        public Analistas Insertar(Analistas entidad)
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

            this.conexion.Analistas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el analista Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        private bool Validar(Analistas entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
