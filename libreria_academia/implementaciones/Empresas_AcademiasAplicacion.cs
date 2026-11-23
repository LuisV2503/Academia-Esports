using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Empresas_AcademiasAplicacion : IEmpresas_AcademiasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public Empresas_AcademiasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Empresas_Academias Actualizar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Empresas_Academias!
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

            if (this.conexion.Empresas_Academias!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Empresas_Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la relación empresa-academia Id {entidad.Id} (Empresa {entidad.Empresa}, Academia {entidad.Academia})");
            return entidad;
        }

        public Empresas_Academias Borrar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Empresas_Academias!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Empresas_Academias!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la relación empresa-academia Id {anterior.Id} (Empresa {anterior.Empresa}, Academia {anterior.Academia})");
            return entidad;
        }

        public List<Empresas_Academias> Buscar(Empresas_Academias entidad)
        {
            var nombre = entidad._empresas?.Nombre ?? "";

            return this.conexion.Empresas_Academias!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .Where(x => x._empresas!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Empresas_Academias> Consultar()
        {
            return this.conexion.Empresas_Academias!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .ToList();
        }

        public Empresas_Academias Insertar(Empresas_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Empresas_Academias!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            this.conexion.Empresas_Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la relación empresa-academia Id {entidad.Id} (Empresa {entidad.Empresa}, Academia {entidad.Academia})");
            return entidad;
        }

        private bool Validar(Empresas_Academias entidad)
        {
            return
                entidad.Empresa == null || entidad.Empresa == 0 ||
                entidad.Academia == null || entidad.Academia == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
