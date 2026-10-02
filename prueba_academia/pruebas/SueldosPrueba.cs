using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class SueldosPrueba
    {
        private Conexion conexion;
        private Sueldos entidad;

        public SueldosPrueba()
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
            entidad = new Sueldos()
            {
                Valor = 999999,
            };

            this.conexion.Sueldos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Sueldos = this.conexion.Sueldos!.ToList();
            if (lista_Sueldos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Valor = 1000000;

            var entry = this.conexion!.Entry<Sueldos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Sueldos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
