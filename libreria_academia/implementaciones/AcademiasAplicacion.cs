using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class AcademiasAplicacion : IAcademiasAplicacion
    {
        private IConexion conexion;

        public AcademiasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Academias Actualizar(Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            if (entidad.Nacional == null)
                entidad.Nacional = false;

            var entry = this.conexion!.Entry<Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Academias Borrar(Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Equipos_Academias!.Any(x => x.Academia == entidad.Id) ||
                this.conexion.Empresas_Academias!.Any(x => x.Academia == entidad.Id) ||
                this.conexion.Empresas_Academias_Eventos!.Any(x => x.Academia == entidad.Id))
                throw new Exception("Academia en uso");

            this.conexion.Academias!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Academias> Buscar(Academias entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Academias!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Academias> Consultar()
        {
            return this.conexion.Academias!
                .ToList();
        }

        public Academias Insertar(Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (entidad.FechaCrea == null)
                entidad.FechaCrea = DateTime.Now;

            if (entidad.Nacional == null)
                entidad.Nacional = false;

            this.conexion.Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Academias entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Correo) ||
                string.IsNullOrEmpty(entidad.Descripcion);
        }
    }
}
