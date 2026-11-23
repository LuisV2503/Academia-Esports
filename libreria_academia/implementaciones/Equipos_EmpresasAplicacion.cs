using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Equipos_EmpresasAplicacion : IEquipos_EmpresasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public Equipos_EmpresasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Equipos_Empresas Actualizar(Equipos_Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Equipos_Empresas!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (this.conexion.Equipos_Empresas!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Empresa == entidad.Empresa))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Equipos_Empresas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó la relación equipo-empresa Id {entidad.Id} (Equipo {entidad.Equipo}, Empresa {entidad.Empresa})");
            return entidad;
        }

        public Equipos_Empresas Borrar(Equipos_Empresas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Equipos_Empresas!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Equipos_Empresas!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró la relación equipo-empresa Id {anterior.Id} (Equipo {anterior.Equipo}, Empresa {anterior.Empresa})");
            return entidad;
        }

        public List<Equipos_Empresas> Buscar(Equipos_Empresas entidad)
        {
            var nombre = entidad._equipos?.Nombre ?? "";

            return this.conexion.Equipos_Empresas!
                .Include(x => x._equipos)
                .Include(x => x._empresas)
                .Where(x => x._equipos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Equipos_Empresas> Consultar()
        {
            return this.conexion.Equipos_Empresas!
                .Include(x => x._equipos)
                .Include(x => x._empresas)
                .ToList();
        }

        public Equipos_Empresas Insertar(Equipos_Empresas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (this.conexion.Equipos_Empresas!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Empresa == entidad.Empresa))
                throw new Exception("Relacion repetida");

            this.conexion.Equipos_Empresas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó la relación equipo-empresa Id {entidad.Id} (Equipo {entidad.Equipo}, Empresa {entidad.Empresa})");
            return entidad;
        }

        private bool Validar(Equipos_Empresas entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Empresa == null || entidad.Empresa == 0 ||
                string.IsNullOrEmpty(entidad.Imagen);
        }
    }
}
