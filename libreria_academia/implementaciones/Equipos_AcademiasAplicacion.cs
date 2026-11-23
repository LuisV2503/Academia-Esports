using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Equipos_AcademiasAplicacion : IEquipos_AcademiasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public Equipos_AcademiasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Equipos_Academias Actualizar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Equipos_Academias!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Equipos_Academias!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Equipos_Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la relación equipo-academia Id {entidad.Id} (Equipo {entidad.Equipo}, Academia {entidad.Academia})");
            return entidad;
        }

        public Equipos_Academias Borrar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Equipos_Academias!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Equipos_Academias!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la relación equipo-academia Id {anterior.Id} (Equipo {anterior.Equipo}, Academia {anterior.Academia})");
            return entidad;
        }

        public List<Equipos_Academias> Buscar(Equipos_Academias entidad)
        {
            var nombre = entidad._equipos?.Nombre ?? "";

            return this.conexion.Equipos_Academias!
                .Include(x => x._equipos)
                .Include(x => x._academias)
                .Where(x => x._equipos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Equipos_Academias> Consultar()
        {
            return this.conexion.Equipos_Academias!
                .Include(x => x._equipos)
                .Include(x => x._academias)
                .ToList();
        }

        public Equipos_Academias Insertar(Equipos_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Equipos_Academias!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            this.conexion.Equipos_Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la relación equipo-academia Id {entidad.Id} (Equipo {entidad.Equipo}, Academia {entidad.Academia})");
            return entidad;
        }

        private bool Validar(Equipos_Academias entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Academia == null || entidad.Academia == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
