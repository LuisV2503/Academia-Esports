using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class FisioterapeutasAplicacion : IFisioterapeutasAplicacion
    {
        private IConexion conexion;

        public FisioterapeutasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Fisioterapeutas Actualizar(Fisioterapeutas entidad)
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

            var entry = this.conexion!.Entry<Fisioterapeutas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Fisioterapeutas Borrar(Fisioterapeutas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Fisioterapeutas!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Fisioterapeutas> Buscar(Fisioterapeutas entidad)
        {
            var nombre = entidad._persona?.Nombre ?? "";

            return this.conexion.Fisioterapeutas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .Where(x => x._persona!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Fisioterapeutas> Consultar()
        {
            return this.conexion.Fisioterapeutas!
                .Include(x => x._persona)
                .Include(x => x._equipo)
                .ToList();
        }

        public Fisioterapeutas Insertar(Fisioterapeutas entidad)
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

            this.conexion.Fisioterapeutas!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Fisioterapeutas entidad)
        {
            return
                entidad.Persona == null || entidad.Persona == 0 ||
                entidad.Equipo == null || entidad.Equipo == 0;
        }
    }
}
