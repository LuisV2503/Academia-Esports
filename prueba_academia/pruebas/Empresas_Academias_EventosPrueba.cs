using libreria_academia.entidades;
using libreria_academia.implementaciones;
using libreria_academia.interfaces;
using libreria_academia.nucleo;
using Microsoft.EntityFrameworkCore;

namespace presentacion_pruebas
{
    [TestClass]
    public class Empresas_Academias_EventosPrueba
    {
        private Conexion conexion;
        private Empresas_Academias_Eventos entidad;

        public Empresas_Academias_EventosPrueba()
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
            entidad = new Empresas_Academias_Eventos()
            {
                Empresa = 1,
                Academia = 1,
                Evento = 1,
            };

            this.conexion.Empresas_Academias_Eventos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_Empresas_Academias_Eventos = this.conexion.Empresas_Academias_Eventos!.ToList();
            if (lista_Empresas_Academias_Eventos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            // No hay un campo descriptivo propio; se reafirma el valor para disparar el UPDATE.
            this.entidad!.Empresa = 1;

            var entry = this.conexion!.Entry<Empresas_Academias_Eventos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Empresas_Academias_Eventos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
