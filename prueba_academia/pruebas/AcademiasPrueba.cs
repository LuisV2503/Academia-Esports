using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class AcademiasPrueba
    {
        private Conexion conexion;
        private Academias entidad;

        public AcademiasPrueba()
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
            entidad = new Academias()
            {
                Nombre = "Test",
                Correo = "test@test.com",
                Descripcion = "Test",
                FechaCrea = DateTime.Now,
                Nacional = true,
            };

            this.conexion.Academias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Academias = this.conexion.Academias!.ToList();
            if (lista_Academias.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Academias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Academias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
