using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class OrganizadoresPrueba
    {
        private Conexion conexion;
        private Organizadores entidad;

        public OrganizadoresPrueba()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Dbconexion.StringConexion();
        }

        [TestMethod]
        public void Ejecutar()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        private void Insertar()
        {
            entidad = new Organizadores()
            {
                Activo = true,
                Persona = 1,
            };

            this.conexion.Organizadores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Organizadores = this.conexion.Organizadores!.ToList();
            if (lista_Organizadores.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Organizadores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Organizadores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
