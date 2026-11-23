using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class TipoAplicacion : ITipoAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public TipoAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Tipo Actualizar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Tipo!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            var entry = this.conexion!.Entry<Tipo>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el tipo {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Tipo Borrar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Empresas!.Any(x => x.Tipo == entidad.Id))
                throw new Exception("Tipo en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Tipo!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Tipo!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el tipo {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Tipo> Buscar(Tipo entidad)
        {
            var nombre = entidad.Nombre ?? "";

            var lista = this.conexion.Tipo!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();

            return lista;
        }

        public List<Tipo> Consultar()
        {
            var lista = this.conexion.Tipo!
                .ToList();

            return lista;
        }

        public Tipo Insertar(Tipo entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            this.conexion.Tipo!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el tipo {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Tipo entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripccion);
        }
    }
}
