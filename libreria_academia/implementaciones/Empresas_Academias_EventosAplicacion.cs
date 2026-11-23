using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Empresas_Academias_EventosAplicacion : IEmpresas_Academias_EventosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public Empresas_Academias_EventosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Empresas_Academias_Eventos Actualizar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Empresas_Academias_Eventos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (this.conexion.Empresas_Academias_Eventos!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia &&
                x.Evento == entidad.Evento))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Empresas_Academias_Eventos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la relación empresa-academia-evento Id {entidad.Id} (Empresa {entidad.Empresa}, Academia {entidad.Academia}, Evento {entidad.Evento})");
            return entidad;
        }

        public Empresas_Academias_Eventos Borrar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Empresas_Academias_Eventos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Empresas_Academias_Eventos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la relación empresa-academia-evento Id {anterior.Id} (Empresa {anterior.Empresa}, Academia {anterior.Academia}, Evento {anterior.Evento})");
            return entidad;
        }

        public List<Empresas_Academias_Eventos> Buscar(Empresas_Academias_Eventos entidad)
        {
            var nombre = entidad._eventos?.Nombre ?? "";

            return this.conexion.Empresas_Academias_Eventos!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .Include(x => x._eventos)
                .Where(x => x._eventos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Empresas_Academias_Eventos> Consultar()
        {
            return this.conexion.Empresas_Academias_Eventos!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .Include(x => x._eventos)
                .ToList();
        }

        public Empresas_Academias_Eventos Insertar(Empresas_Academias_Eventos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (!this.conexion.Eventos!.Any(x => x.Id == entidad.Evento))
                throw new Exception("Evento invalido");

            if (this.conexion.Empresas_Academias_Eventos!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia &&
                x.Evento == entidad.Evento))
                throw new Exception("Relacion repetida");

            this.conexion.Empresas_Academias_Eventos!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la relación empresa-academia-evento Id {entidad.Id} (Empresa {entidad.Empresa}, Academia {entidad.Academia}, Evento {entidad.Evento})");
            return entidad;
        }

        private bool Validar(Empresas_Academias_Eventos entidad)
        {
            return
                entidad.Empresa == null || entidad.Empresa == 0 ||
                entidad.Academia == null || entidad.Academia == 0 ||
                entidad.Evento == null || entidad.Evento == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
