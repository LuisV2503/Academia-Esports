using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EmpresasAplicacion : IEmpresasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public EmpresasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Empresas Actualizar(Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Empresas!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Tipo!.Any(x => x.Id == entidad.Tipo))
                throw new Exception("Tipo invalido");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            var entry = this.conexion!.Entry<Empresas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la empresa {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Empresas Borrar(Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Equipos_Empresas!.Any(x => x.Empresa == entidad.Id) ||
                this.conexion.Empresas_Academias!.Any(x => x.Empresa == entidad.Id) ||
                this.conexion.Empresas_Academias_Eventos!.Any(x => x.Empresa == entidad.Id))
                throw new Exception("Empresa en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Empresas!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Empresas!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la empresa {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Empresas> Buscar(Empresas entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Empresas!
                .Include(x => x._tipo)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Empresas> Consultar()
        {
            return this.conexion.Empresas!
                .Include(x => x._tipo)
                .ToList();
        }

        public Empresas Insertar(Empresas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Tipo!.Any(x => x.Id == entidad.Tipo))
                throw new Exception("Tipo invalido");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            this.conexion.Empresas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la empresa {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Empresas entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Pais) ||
                string.IsNullOrEmpty(entidad.Descripcion) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                entidad.Tipo == null || entidad.Tipo == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
