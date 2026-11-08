using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Equipos_AcademiasAplicacion : IEquipos_AcademiasAplicacion
    {
        private IConexion conexion;

        public Equipos_AcademiasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Equipos_Academias Actualizar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Equipos_Academias!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Equipos_Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Equipos_Academias Borrar(Equipos_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Equipos_Academias!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Equipos_Academias> Buscar(Equipos_Academias entidad)
        {
            var nombre = entidad._equipos?.Nombre ?? "";

            return this.conexion.Equipos_Academias!
                .Include(x => x._equipos)
                .Include(x => x._academias)
                .Where(x => x._equipos!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Equipos_Academias> Consultar()
        {
            return this.conexion.Equipos_Academias!
                .Include(x => x._equipos)
                .Include(x => x._academias)
                .ToList();
        }

        public Equipos_Academias Insertar(Equipos_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Equipos!.Any(x => x.Id == entidad.Equipo))
                throw new Exception("Equipo invalido");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Equipos_Academias!.Any(x => x.Id != entidad.Id &&
                x.Equipo == entidad.Equipo &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            this.conexion.Equipos_Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Equipos_Academias entidad)
        {
            return
                entidad.Equipo == null || entidad.Equipo == 0 ||
                entidad.Academia == null || entidad.Academia == 0;
        }
    }
}
