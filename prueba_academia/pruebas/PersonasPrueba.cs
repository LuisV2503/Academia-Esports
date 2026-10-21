using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class PersonasPrueba
    {
        private Conexion conexion;
        private Personas entidad;
        private Sueldos sueldoEntidad;

        public PersonasPrueba()
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
            // La columna Sueldo de Personas es UNIQUE (relación 1 a 1),
            // así que la prueba crea su propio Sueldo en vez de reutilizar uno existente.
            sueldoEntidad = new Sueldos()
            {
                Valor = 1000000,
            };

            this.conexion.Sueldos!.Add(this.sueldoEntidad!);
            this.conexion.SaveChanges();

            entidad = new Personas()
            {
                Nombre = "Test",
                FechaNacimiento = DateTime.Now,
                Nacionalidad = "Test",
                Sueldo = this.sueldoEntidad.Id,
            };

            this.conexion.Personas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Personas = this.conexion.Personas!.ToList();
            if (lista_Personas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Personas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Personas!.Remove(this.entidad!);
            this.conexion.SaveChanges();

            this.conexion.Sueldos!.Remove(this.sueldoEntidad!);
            this.conexion.SaveChanges();
        }
    }
}
