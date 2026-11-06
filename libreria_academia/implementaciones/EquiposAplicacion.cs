using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class EquiposAplicacion : IEquiposAplicacion
    {
        private IConexion conexion;

        public EquiposAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Equipos Actualizar(Equipos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            var entry = this.conexion!.Entry<Equipos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
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

            this.conexion.Equipos!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Equipos> Buscar(Equipos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Equipos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Equipos> Consultar()
        {
            return this.conexion.Equipos!
                .ToList();
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
