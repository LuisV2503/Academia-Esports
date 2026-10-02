using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class Equipos_JuegosPrueba
    {
        private Conexion conexion;
        private Equipos_Juegos entidad;

        public Equipos_JuegosPrueba()
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
            entidad = new Equipos_Juegos()
            {
                Equipo = 1,
                Juego = 1,
            };

            this.conexion.Equipos_Juegos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Equipos_Juegos = this.conexion.Equipos_Juegos!.ToList();
            if (lista_Equipos_Juegos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            // No hay un campo descriptivo propio; se reafirma el valor para disparar el UPDATE.
            this.entidad!.Equipo = 1;

            var entry = this.conexion!.Entry<Equipos_Juegos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Equipos_Juegos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
