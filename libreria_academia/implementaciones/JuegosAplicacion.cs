using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class JuegosAplicacion : IJuegosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public JuegosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Juegos Actualizar(Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Juegos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.Pago == null)
                entidad.Pago = false;

            var entry = this.conexion!.Entry<Juegos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el juego {entidad.Nombre} (Id {entidad.Id})");
            return entidad;
        }

        public Juegos Borrar(Juegos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Equipos_Juegos!.Any(x => x.Juego == entidad.Id))
                throw new Exception("Juego en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Juegos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Juegos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el juego {anterior.Nombre} (Id {anterior.Id})");
            return entidad;
        }

        public List<Juegos> Buscar(Juegos entidad)
        {
            var nombre = entidad.Nombre ?? "";

            var lista = this.conexion.Juegos!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();

            return lista;
        }

        public List<Juegos> Consultar()
        {
            var lista = this.conexion.Juegos!
                .ToList();

            return lista;
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

            this.auditoria.Registrar($"insertó el juego {entidad.Nombre} (Id {entidad.Id})");
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
