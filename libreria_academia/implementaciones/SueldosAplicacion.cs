using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_academia.implementaciones
{
    public class SueldosAplicacion : ISueldosAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoria;

        public SueldosAplicacion(IConexion conexion, IAuditoriasAplicacion auditoria)
        {
            this.conexion = conexion;
            this.auditoria = auditoria;
        }

        public Sueldos Actualizar(Sueldos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            // Si no se elige una imagen nueva, se conserva la que ya tiene
            if (string.IsNullOrEmpty(entidad.Imagen))
            {
                entidad.Imagen = this.conexion.Sueldos!
                    .Where(x => x.Id == entidad.Id)
                    .Select(x => x.Imagen)
                    .FirstOrDefault();
            }

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            var entry = this.conexion!.Entry<Sueldos>(entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();

            this.auditoria.Registrar($"actualizó el sueldo {entidad.Valor} (Id {entidad.Id})");
            return entidad;
        }

        public Sueldos Borrar(Sueldos entidad)
        {
            if (entidad.Id == null || entidad.Id == 0)
                throw new Exception("No se guardo");

            if (this.conexion.Personas!.Any(x => x.Sueldo == entidad.Id))
                throw new Exception("Sueldo en uso");

            // Se lee el registro antes de borrarlo para poder nombrarlo en la auditoría
            var anterior = this.conexion.Sueldos!
                .FirstOrDefault(x => x.Id == entidad.Id) ?? entidad;

            this.conexion.Sueldos!.Remove(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"borró el sueldo {anterior.Valor} (Id {anterior.Id})");
            return entidad;
        }

        public List<Sueldos> Buscar(Sueldos entidad)
        {
            if (entidad.Valor == null || entidad.Valor == 0)
                return this.conexion.Sueldos!.ToList();

            return this.conexion.Sueldos!
                .Where(x => x.Valor == entidad.Valor)
                .ToList();
        }

        public List<Sueldos> Consultar()
        {
            return this.conexion.Sueldos!
                .ToList();
        }

        public Sueldos Insertar(Sueldos entidad)
        {
            if (entidad.Id != null && entidad.Id != 0)
                throw new Exception("Ya se guardo");

            if (Validar(entidad))
                throw new Exception("Faltan valores");

            this.conexion.Sueldos!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoria.Registrar($"insertó el sueldo {entidad.Valor} (Id {entidad.Id})");
            return entidad;
        }

        private bool Validar(Sueldos entidad)
        {
            return
                entidad.Valor == null || entidad.Valor == 0;
        }
    }
}
