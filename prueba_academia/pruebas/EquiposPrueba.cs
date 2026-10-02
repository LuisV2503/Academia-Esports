using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class EquiposPrueba
    {
        private Conexion conexion;
        private Equipos entidad;

        public EquiposPrueba()
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
            entidad = new Equipos()
            {
                Nombre = "Test",
                Siglas = "TST",
                Descripcion = "Test",
                FechaCrea = DateTime.Now,
            };

            this.conexion.Equipos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Equipos = this.conexion.Equipos!.ToList();
            if (lista_Equipos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Equipos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Equipos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
