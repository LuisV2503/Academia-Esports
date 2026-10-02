using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class EntrenadoresPrueba
    {
        private Conexion conexion;
        private Entrenadores entidad;

        public EntrenadoresPrueba()
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
            entidad = new Entrenadores()
            {
                Activo = true,
                Persona = 1,
                Equipo = 1,
            };

            this.conexion.Entrenadores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Entrenadores = this.conexion.Entrenadores!.ToList();
            if (lista_Entrenadores.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Activo = false;

            var entry = this.conexion!.Entry<Entrenadores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Entrenadores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
