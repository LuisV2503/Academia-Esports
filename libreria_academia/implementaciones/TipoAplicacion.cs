using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class TipoAplicacion : ITipoAplicacion
    {
        private IConexion conexion;

        public TipoAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Tipo Actualizar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            var entry = this.conexion!.Entry<Tipo>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Tipo Borrar(Tipo entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Empresas!.Any(x => x.Tipo == entidad.Id))
                throw new Exception("Tipo en uso");

            this.conexion.Tipo!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Tipo> Buscar(Tipo entidad)
        {
            var nombre = entidad.Nombre ?? "";

            return this.conexion.Tipo!
                .Where(x => x.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Tipo> Consultar()
        {
            return this.conexion.Tipo!
                .ToList();
        }

        public Tipo Insertar(Tipo entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            this.conexion.Tipo!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Tipo entidad)
        {
            return
                string.IsNullOrEmpty(entidad.Nombre) ||
                string.IsNullOrEmpty(entidad.Descripccion);
        }
    }
}
