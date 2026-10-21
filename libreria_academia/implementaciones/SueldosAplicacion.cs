using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_academia.implementaciones
{
    public class SueldosAplicacion : ISueldosAplicacion
    {
        private IConexion conexion;

        public SueldosAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Sueldos Actualizar(Sueldos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (entidad.Valor == null || entidad.Valor == 0)
                throw new Exception("Faltan valores");


            var entry = this.conexion!.Entry<Sueldos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Sueldos Borrar(Sueldos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Sueldos!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Sueldos> Consultar()
        {
            return this.conexion.Sueldos!
                .ToList();
        }

        public Sueldos Insertar(Sueldos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (entidad.Valor == null || entidad.Valor == 0)
                throw new Exception("Faltan valores");

            this.conexion.Sueldos!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }
    }
}

