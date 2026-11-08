using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class AnalistasAplicacion : IAnalistasAplicacion
    {
        private IConexion conexion;

        public AnalistasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Analistas Actualizar(Analistas entidad)
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

            var entry = this.conexion!.Entry<Analistas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Analistas Borrar(Analistas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Analistas!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Analistas> Buscar(Analistas entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Analistas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Analistas> Consultar()
        {
            return this.conexion.Analistas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();
        }

        public Analistas Insertar(Analistas entidad)
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

            this.conexion.Analistas!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Analistas entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
