using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class JugadoresAplicacion : IJugadoresAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public JugadoresAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Jugadores Actualizar(Jugadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            var entry = this.conexion!.Entry<Jugadores>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el jugador Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        public Jugadores Borrar(Jugadores entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Jugadores!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Jugadores!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el jugador Id {anterior.Id} (Persona {anterior.Persona}, Equipo {anterior.Equipo})");
            return entidad;
        }

        public List<Jugadores> Buscar(Jugadores entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            var lista = this.conexion.Jugadores!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();

            // La foto del rol es la de su persona
            foreach (var elemento in lista)
                elemento.Imagen = elemento._persona?.Imagen;

            return lista;
        }

        public List<Jugadores> Consultar()
        {
            var lista = this.conexion.Jugadores!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();

            // La foto del rol es la de su persona
            foreach (var elemento in lista)
                elemento.Imagen = elemento._persona?.Imagen;

            return lista;
        }

        public Jugadores Insertar(Jugadores entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Personas!.Any(x => x.Id == entidad.Persona))
                throw new Exception("Persona invalida");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (entidad.Activo == null)
                entidad.Activo = false;

            this.conexion.Jugadores!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el jugador Id {entidad.Id} (Persona {entidad.Persona}, Equipo {entidad.Equipo})");
            return entidad;
        }

        private bool Validar(Jugadores entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
