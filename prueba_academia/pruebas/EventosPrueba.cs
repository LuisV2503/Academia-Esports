using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class EventosPrueba
    {
        private Conexion conexion;
        private Eventos entidad;

        public EventosPrueba()
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
            entidad = new Eventos()
            {
                Nombre = "Test",
                Descripcion = "Test",
                FechaIni = DateTime.Now,
                FechaFin = DateTime.Now,
                Activo = true,
                Modalidad = "Test",
                Ciudad = "Test",
                Direccion = "Test",
            };

            this.conexion.Eventos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Eventos = this.conexion.Eventos!.ToList();
            if (lista_Eventos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Nombre = "Test.1";

            var entry = this.conexion!.Entry<Eventos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Eventos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
