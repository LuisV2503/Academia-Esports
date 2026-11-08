using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class Empresas_AcademiasAplicacion : IEmpresas_AcademiasAplicacion
    {
        private IConexion conexion;

        public Empresas_AcademiasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Empresas_Academias Actualizar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Empresas_Academias!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            var entry = this.conexion!.Entry<Empresas_Academias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
            return entidad;
        }

        public Empresas_Academias Borrar(Empresas_Academias entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            this.conexion.Empresas_Academias!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Empresas_Academias> Buscar(Empresas_Academias entidad)
        {
            var nombre = entidad._empresas?.Nombre ?? "";

            return this.conexion.Empresas_Academias!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .Where(x => x._empresas!.Nombre!.Contains(nombre))
                .ToList();
        }

        public List<Empresas_Academias> Consultar()
        {
            return this.conexion.Empresas_Academias!
                .Include(x => x._empresas)
                .Include(x => x._academias)
                .ToList();
        }

        public Empresas_Academias Insertar(Empresas_Academias entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            if (!this.conexion.Empresas!.Any(x => x.Id == entidad.Empresa))
                throw new Exception("Empresa invalida");

            if (!this.conexion.Academias!.Any(x => x.Id == entidad.Academia))
                throw new Exception("Academia invalida");

            if (this.conexion.Empresas_Academias!.Any(x => x.Id != entidad.Id &&
                x.Empresa == entidad.Empresa &&
                x.Academia == entidad.Academia))
                throw new Exception("Relacion repetida");

            this.conexion.Empresas_Academias!.Add(entidad!);
            this.conexion.SaveChanges();

            return entidad;
        }

        private bool Validar(Empresas_Academias entidad)
        {
            return
                entidad.Empresa == null || entidad.Empresa == 0 ||
                entidad.Academia == null || entidad.Academia == 0;
        }
    }
}
