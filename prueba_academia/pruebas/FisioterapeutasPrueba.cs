using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class FisioterapeutasPrueba
    {
        private Conexion conexion;
        private Fisioterapeutas entidad;

        public FisioterapeutasPrueba()
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
            entidad = new Fisioterapeutas()
            {
                Activo = true,
                Persona = 1,
                Equipo = 1,
            };

            this.conexion.Fisioterapeutas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Fisioterapeutas = this.conexion.Fisioterapeutas!.ToList();
            if (lista_Fisioterapeutas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Fisioterapeutas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Fisioterapeutas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
