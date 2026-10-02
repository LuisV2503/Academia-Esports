using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class EmpresasPrueba
    {
        private Conexion conexion;
        private Empresas entidad;

        public EmpresasPrueba()
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
            entidad = new Empresas()
            {
                Nombre = "Test",
                Pais = "Test",
                Descripcion = "Test",
                FechaCrea = DateTime.Now,
                Correo = "test@test.com",
                Tipo = 1,
            };

            this.conexion.Empresas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Empresas = this.conexion.Empresas!.ToList();
            if (lista_Empresas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Empresas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empresas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
