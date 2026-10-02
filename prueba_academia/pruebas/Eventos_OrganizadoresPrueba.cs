using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class Eventos_OrganizadoresPrueba
    {
        private Conexion conexion;
        private Eventos_Organizadores entidad;

        public Eventos_OrganizadoresPrueba()
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
            entidad = new Eventos_Organizadores()
            {
                Evento = 1,
                Organizador = 1,
            };

            this.conexion.Eventos_Organizadores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Eventos_Organizadores = this.conexion.Eventos_Organizadores!.ToList();
            if (lista_Eventos_Organizadores.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            // No hay un campo descriptivo propio; se reafirma el valor para disparar el UPDATE.
            this.entidad!.Evento = 1;

            var entry = this.conexion!.Entry<Eventos_Organizadores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Eventos_Organizadores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
