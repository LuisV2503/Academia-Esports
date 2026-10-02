using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class EntretenimientosPrueba
    {
        private Conexion conexion;
        private Entretenimientos entidad;

        public EntretenimientosPrueba()
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
            entidad = new Entretenimientos()
            {
                Nombre = "Test",
                Descripcion = "Test",
                Duracion = 30,
                Evento = 1,
            };

            this.conexion.Entretenimientos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Entretenimientos = this.conexion.Entretenimientos!.ToList();
            if (lista_Entretenimientos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Entretenimientos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Entretenimientos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
