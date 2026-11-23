using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class PersonasAplicacion : IPersonasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public PersonasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Personas Actualizar(Personas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Personas!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Sueldos!.Any(x => x.Id == entidad.Sueldo))
                throw new Exception("Sueldo invalido");

            if (entidad.FechaNacimiento == null)
                entidad.FechaNacimiento = DateTime.Now;

            var entry = this.conexion!.Entry<Personas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la persona {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Jugadores!.Any(x => x.Persona == entidad.Id) ||
                this.conexion.Entrenadores!.Any(x => x.Persona == entidad.Id) ||
                this.conexion.Analistas!.Any(x => x.Persona == entidad.Id) ||
                this.conexion.Psicologos!.Any(x => x.Persona == entidad.Id) ||
                this.conexion.Fisioterapeutas!.Any(x => x.Persona == entidad.Id) ||
                this.conexion.Organizadores!.Any(x => x.Persona == entidad.Id))
                throw new Exception("Persona en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Personas!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Personas!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la persona {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Personas> Buscar(Personas entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Personas!
                .Include(x => x._sueldo)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Personas> Consultar()
        {
            return this.conexion.Personas!
                .Include(x => x._sueldo)
                .ToList();
        }

        public Personas Insertar(Personas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Sueldos!.Any(x => x.Id == entidad.Sueldo))
                throw new Exception("Sueldo invalido");

            if (entidad.FechaNacimiento == null)
                entidad.FechaNacimiento = DateTime.Now;

            this.conexion.Personas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la persona {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Personas entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Nacionalidad) ||
                entidad.Sueldo == null || entidad.Sueldo == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
