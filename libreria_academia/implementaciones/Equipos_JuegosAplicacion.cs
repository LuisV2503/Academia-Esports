using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Equipos_JuegosAplicacion : IEquipos_JuegosAplicacion
    {
        private IConexion conexion;

        public Equipos_JuegosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Equipos_Juegos Actualizar(Equipos_Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Juegos!.Any(x => x.Id == entidad.Juego))
                throw new Exception("Juego invalido");

            if (this.conexion.Equipos_Juegos!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Juego == entidad.Juego))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Equipos_Juegos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Equipos_Juegos Borrar(Equipos_Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Equipos_Juegos!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Equipos_Juegos> Buscar(Equipos_Juegos entidad)
        {
            var nombre = entidad._equipos?.Nombre ?? "";

            return this.conexion.Equipos_Juegos!
                .Include(x => x._equipos)
                .Include(x => x._juegos)
                .Where(x => x._equipos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Equipos_Juegos> Consultar()
        {
            return this.conexion.Equipos_Juegos!
                .Include(x => x._equipos)
                .Include(x => x._juegos)
                .ToList();
        }

        public Equipos_Juegos Insertar(Equipos_Juegos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Juegos!.Any(x => x.Id == entidad.Juego))
                throw new Exception("Juego invalido");

            if (this.conexion.Equipos_Juegos!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Juego == entidad.Juego))
                throw new Exception("Relacion repetida");

            this.conexion.Equipos_Juegos!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Equipos_Juegos entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Juego == null || entidad.Juego == 0;
        }
    }
}
