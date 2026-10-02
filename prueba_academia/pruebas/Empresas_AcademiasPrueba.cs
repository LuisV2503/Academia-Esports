using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class Empresas_AcademiasPrueba
    {
        private Conexion conexion;
        private Empresas_Academias entidad;

        public Empresas_AcademiasPrueba()
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
            entidad = new Empresas_Academias()
            {
                Empresa = 1,
                Academia = 1,
            };

            this.conexion.Empresas_Academias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Empresas_Academias = this.conexion.Empresas_Academias!.ToList();
            if (lista_Empresas_Academias.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            // No hay un campo descriptivo propio; se reafirma el valor para disparar el UPDATE.
            this.entidad!.Empresa = 1;

            var entry = this.conexion!.Entry<Empresas_Academias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empresas_Academias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
