using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EquiposAplicacion : IEquiposAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public EquiposAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Equipos Actualizar(Equipos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Equipos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            var entry = this.conexion!.Entry<Equipos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el equipo {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Equipos Borrar(Equipos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Jugadores!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Entrenadores!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Analistas!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Psicologos!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Fisioterapeutas!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Equipos_Juegos!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Equipos_Empresas!.Any(x => x.Equipo == entidad.Id) ||
                this.conexion.Equipos_Academias!.Any(x => x.Equipo == entidad.Id))
                throw new Exception("Equipo en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Equipos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Equipos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el equipo {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Equipos> Buscar(Equipos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            var lista = this.conexion.Equipos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();

            return lista;
        }

        public List<Equipos> Consultar()
        {
            var lista = this.conexion.Equipos!
                .ToList();

            return lista;
        }

        public Equipos Insertar(Equipos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            this.conexion.Equipos!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el equipo {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Equipos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Siglas) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}
