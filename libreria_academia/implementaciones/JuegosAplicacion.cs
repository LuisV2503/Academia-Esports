using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class JuegosAplicacion : IJuegosAplicacion
    {
        private IConexion conexion;

        public JuegosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Juegos Actualizar(Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.Pago == null)
                entidad.Pago = false;

            var entry = this.conexion!.Entry<Juegos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Juegos Borrar(Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Equipos_Juegos!.Any(x => x.Juego == entidad.Id))
                throw new Exception("Juego en uso");

            this.conexion.Juegos!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Juegos> Buscar(Juegos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Juegos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Juegos> Consultar()
        {
            return this.conexion.Juegos!
                .ToList();
        }

        public Juegos Insertar(Juegos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.Pago == null)
                entidad.Pago = false;

            this.conexion.Juegos!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Juegos entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Genero) ||
                entidad.CantJug == null || entidad.CantJug == 0;
        }
    }
}
