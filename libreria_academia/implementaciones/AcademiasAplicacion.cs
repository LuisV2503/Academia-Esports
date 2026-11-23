using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class AcademiasAplicacion : IAcademiasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public AcademiasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Academias Actualizar(Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Academias!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            if (entidad.Nacional == null)
                entidad.Nacional = false;

            var entry = this.conexion!.Entry<Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la academia {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Academias Borrar(Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Equipos_Academias!.Any(x => x.Academia == entidad.Id) ||
                this.conexion.Empresas_Academias!.Any(x => x.Academia == entidad.Id) ||
                this.conexion.Empresas_Academias_Eventos!.Any(x => x.Academia == entidad.Id))
                throw new Exception("Academia en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Academias!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Academias!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la academia {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Academias> Buscar(Academias entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Academias!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Academias> Consultar()
        {
            return this.conexion.Academias!
                .ToList();
        }

        public Academias Insertar(Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            if (entidad.Nacional == null)
                entidad.Nacional = false;

            this.conexion.Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la academia {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Academias entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
