using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class FisioterapeutasAplicacion : IFisioterapeutasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public FisioterapeutasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Fisioterapeutas Actualizar(Fisioterapeutas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Fisioterapeutas!
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

            var entry = this.conexion!.Entry<Fisioterapeutas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el fisioterapeuta Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        public Fisioterapeutas Borrar(Fisioterapeutas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Fisioterapeutas!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Fisioterapeutas!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el fisioterapeuta Id {anterior.Id} (Persona {anterior.Persona}, Equipo {anterior.Equipo})");
            return entidad;
        }

        public List<Fisioterapeutas> Buscar(Fisioterapeutas entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Fisioterapeutas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Fisioterapeutas> Consultar()
        {
            return this.conexion.Fisioterapeutas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();
        }

        public Fisioterapeutas Insertar(Fisioterapeutas entidad)
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

            this.conexion.Fisioterapeutas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el fisioterapeuta Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        private bool Validar(Fisioterapeutas entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
