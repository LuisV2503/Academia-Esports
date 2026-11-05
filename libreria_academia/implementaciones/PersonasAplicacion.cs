using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class PersonasAplicacion : IPersonasAplicacion
    {
        private IConexion conexion;

        public PersonasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Personas Actualizar(Personas entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Nacionalidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaNacimiento == null)
                entidad.FechaNacimiento = DateTime.Now;

            if (!this.conexion.Sueldos!.Any(x => x.Id == entidad.Sueldo))
                throw new Exception("Sueldo invalido");

            var entry = this.conexion!.Entry<Personas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Personas Borrar(Personas entidad)
        {
            if (entidad.Id == 0 || entidad.Id == null)
                throw new Exception("No se guardo");

            this.conexion.Personas!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Personas> Consultar()
        {
            return this.conexion.Personas!
            .Include(x => x._sueldo)
            .ToList();
        }
            
        public Personas Insertar(Personas entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Nacionalidad) ||
                entidad.Sueldo == null || entidad.Sueldo == 0)
                throw new Exception("Faltan valores");

            if (!this.conexion.Sueldos!.Any(x => x.Id == entidad.Sueldo))
                throw new Exception("Sueldo invalido");

            if (entidad.FechaNacimiento == null)
                entidad.FechaNacimiento = DateTime.Now;

            this.conexion.Personas!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }
        public List<Personas> Buscar(Personas entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Personas!
                .Include(x => x._sueldo)
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }
    }
}
