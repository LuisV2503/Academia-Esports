using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;


namespace libreria_academia.implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion conexion;
        private IUsuarioActual usuarioActual;

        public AuditoriasAplicacion(IConexion conexion, IUsuarioActual usuarioActual)
        {
            this.conexion = conexion;
            this.usuarioActual = usuarioActual;
        }

        public void Registrar(string accion, int? usuario = null)
        {
            var id = usuario ?? this.usuarioActual.Id;

            string? nombre = null;
            if (id != null)
            {
                nombre = this.conexion.Usuarios!
                    .Where(x => x.Id == id)
                    .Select(x => x.Nombre)
                    .FirstOrDefault();
            }

            var quien = nombre ?? "sin identificar";
            var descripcion = Limpiar($"El usuario {quien} {accion}")!;
            if (descripcion.Length > 500)
                descripcion = descripcion.Substring(0, 500);

            var auditoria = new Auditorias()
            {
                Descripcion = descripcion,
                Fecha = DateTime.Now,
                Usuario = nombre != null ? id : null
            };

            this.conexion.Auditorias!.Add(auditoria);
            this.conexion.SaveChanges();
        }

        // Quita comillas y barras: Comunicaciones las convierte y rompen la lectura del JSON
        private static string? Limpiar(string? texto)
        {
            return texto?.Replace(((char)39).ToString(), "").Replace(((char)34).ToString(), "").Replace(((char)92).ToString(), "/");
        }

        // Solo se trae Id y Nombre del usuario (no su imagen) para que la lista sea liviana
        private List<Auditorias> Listar(string texto)
        {
            var lista = this.conexion.Auditorias!
                .Where(x => x.Descripcion!.Contains(texto))
                .OrderByDescending(x => x.Fecha)
                .Select(x => new Auditorias()
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Fecha = x.Fecha,
                    Usuario = x.Usuario,
                    _usuario = x._usuario == null ? null : new Usuarios()
                    {
                        Id = x._usuario.Id,
                        Nombre = x._usuario.Nombre
                    }
                })
                .ToList();

            foreach (var auditoria in lista)
                auditoria.Descripcion = Limpiar(auditoria.Descripcion);
            return lista;
        }

        public List<Auditorias> Consultar()
        {
            return Listar("");
        }

        public List<Auditorias> Buscar(Auditorias entidad)
        {
            return Listar(Limpiar(entidad.Descripcion) ?? "");
        }
    }
}
