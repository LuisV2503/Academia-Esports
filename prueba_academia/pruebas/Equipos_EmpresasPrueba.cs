using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class Equipos_EmpresasPrueba
    {
        private Conexion conexion;
        private Equipos_Empresas entidad;

        public Equipos_EmpresasPrueba()
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
            entidad = new Equipos_Empresas()
            {
                Equipo = 1,
                Empresa = 1,
            };

            this.conexion.Equipos_Empresas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Equipos_Empresas = this.conexion.Equipos_Empresas!.ToList();
            if (lista_Equipos_Empresas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            // No hay un campo descriptivo propio; se reafirma el valor para disparar el UPDATE.
            this.entidad!.Equipo = 1;

            var entry = this.conexion!.Entry<Equipos_Empresas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Equipos_Empresas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
