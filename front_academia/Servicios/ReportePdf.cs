using System.Reflection;
using System.Text.RegularExpressions;
using libreria_academia.entidades;
using presentacion_academia.interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace front_academia.Servicios
{
    // Genera un solo PDF con una hoja (o más, si hay muchas filas) por cada entidad.
    // Las columnas se sacan de las propiedades de cada clase, así que no hay que mantener una lista por tabla.
    public class ReportePdf
    {
        private IServiceProvider servicios;

        public ReportePdf(IServiceProvider servicios)
        {
            this.servicios = servicios;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        private class Seccion
        {
            public string Titulo = "";
            public Type Tipo = typeof(object);
            public List<object> Filas = new List<object>();
        }

        private Seccion Cargar<TEntidad, TPresentacion>(string titulo, Func<TPresentacion, List<TEntidad>> consultar)
            where TPresentacion : notnull
        {
            var presentacion = (TPresentacion)this.servicios.GetService(typeof(TPresentacion))!;
            var lista = consultar(presentacion);
            return new Seccion
            {
                Titulo = titulo,
                Tipo = typeof(TEntidad),
                Filas = lista.Cast<object>().ToList()
            };
        }

        public byte[] Generar(string usuario)
        {
            var secciones = new List<Seccion>()
            {
                Cargar<Sueldos, ISueldosPresentacion>("Sueldos", p => p.Consultar()),
                Cargar<Personas, IPersonasPresentacion>("Personas", p => p.Consultar()),
                Cargar<Equipos, IEquiposPresentacion>("Equipos", p => p.Consultar()),
                Cargar<Jugadores, IJugadoresPresentacion>("Jugadores", p => p.Consultar()),
                Cargar<Entrenadores, IEntrenadoresPresentacion>("Entrenadores", p => p.Consultar()),
                Cargar<Analistas, IAnalistasPresentacion>("Analistas", p => p.Consultar()),
                Cargar<Psicologos, IPsicologosPresentacion>("Psicologos", p => p.Consultar()),
                Cargar<Fisioterapeutas, IFisioterapeutasPresentacion>("Fisioterapeutas", p => p.Consultar()),
                Cargar<Organizadores, IOrganizadoresPresentacion>("Organizadores", p => p.Consultar()),
                Cargar<Juegos, IJuegosPresentacion>("Juegos", p => p.Consultar()),
                Cargar<Equipos_Juegos, IEquipos_JuegosPresentacion>("Equipos y Juegos", p => p.Consultar()),
                Cargar<Tipo, ITipoPresentacion>("Tipo", p => p.Consultar()),
                Cargar<Empresas, IEmpresasPresentacion>("Empresas", p => p.Consultar()),
                Cargar<Equipos_Empresas, IEquipos_EmpresasPresentacion>("Equipos y Empresas", p => p.Consultar()),
                Cargar<Academias, IAcademiasPresentacion>("Academias", p => p.Consultar()),
                Cargar<Equipos_Academias, IEquipos_AcademiasPresentacion>("Equipos y Academias", p => p.Consultar()),
                Cargar<Empresas_Academias, IEmpresas_AcademiasPresentacion>("Empresas y Academias", p => p.Consultar()),
                Cargar<Eventos, IEventosPresentacion>("Eventos", p => p.Consultar()),
                Cargar<Empresas_Academias_Eventos, IEmpresas_Academias_EventosPresentacion>("Empresas, Academias y Eventos", p => p.Consultar()),
                Cargar<Eventos_Organizadores, IEventos_OrganizadoresPresentacion>("Eventos y Organizadores", p => p.Consultar()),
                Cargar<Entretenimientos, IEntretenimientosPresentacion>("Entretenimientos", p => p.Consultar()),
                Cargar<Roles, IRolesPresentacion>("Roles", p => p.Consultar()),
                Cargar<Usuarios, IUsuariosPresentacion>("Usuarios", p => p.Consultar()),
                Cargar<Logins, ILoginsPresentacion>("Logins", p => p.Consultar()),
                Cargar<Auditorias, IAuditoriasPresentacion>("Auditorías", p => p.Consultar()),
            };

            var fecha = DateTime.Now;

            var documento = Document.Create(contenedor =>
            {
                // Portada con el resumen
                contenedor.Page(pagina =>
                {
                    Configurar(pagina);
                    pagina.Content().Column(columna =>
                    {
                        columna.Spacing(8);
                        columna.Item().Text("Reporte general - Academia").FontSize(26).Bold();
                        columna.Item().Text($"Generado el {fecha:dd/MM/yyyy HH:mm} por {usuario}").FontSize(11);
                        columna.Item().PaddingTop(12).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(def =>
                            {
                                def.RelativeColumn(3);
                                def.RelativeColumn(1);
                            });
                            tabla.Header(enc =>
                            {
                                enc.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Entidad").Bold();
                                enc.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text("Registros").Bold();
                            });
                            foreach (var seccion in secciones)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4).Text(seccion.Titulo);
                                tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4).Text(seccion.Filas.Count.ToString());
                            }
                        });
                    });
                });

                // Una página (o más) por entidad
                foreach (var seccion in secciones)
                {
                    contenedor.Page(pagina =>
                    {
                        Configurar(pagina);
                        pagina.Header().PaddingBottom(8).Text(seccion.Titulo).FontSize(18).Bold();
                        pagina.Content().Element(c => DibujarTabla(c, seccion));
                    });
                }
            });

            return documento.GeneratePdf();
        }

        private static void Configurar(PageDescriptor pagina)
        {
            pagina.Size(PageSizes.A4.Landscape());
            pagina.Margin(30);
            pagina.DefaultTextStyle(x => x.FontSize(9));
            pagina.Footer().AlignCenter().Text(texto =>
            {
                texto.Span("Academia - Página ");
                texto.CurrentPageNumber();
            });
        }

        private static void DibujarTabla(IContainer contenedor, Seccion seccion)
        {
            var columnas = Columnas(seccion.Tipo);

            if (seccion.Filas.Count == 0)
            {
                contenedor.Text("Sin registros");
                return;
            }

            contenedor.Table(tabla =>
            {
                tabla.ColumnsDefinition(def =>
                {
                    foreach (var columna in columnas)
                    {
                        if (columna.Name == "Descripcion" || columna.Name == "Descripccion")
                            def.RelativeColumn(3);
                        else
                            def.RelativeColumn(1);
                    }
                });

                tabla.Header(enc =>
                {
                    foreach (var columna in columnas)
                        enc.Cell().Background(Colors.Grey.Lighten2).Padding(4).Text(Etiqueta(columna.Name)).Bold();
                });

                foreach (var fila in seccion.Filas)
                {
                    foreach (var columna in columnas)
                        tabla.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4).Text(Valor(fila, columna));
                }
            });
        }

        // Propiedades simples de la clase (sin navegaciones, listas, imagen ni contraseña)
        private static List<PropertyInfo> Columnas(Type tipo)
        {
            return tipo.GetProperties()
                .Where(p => !p.Name.StartsWith("_") &&
                            p.Name != "Imagen" &&
                            p.Name != "Contrasena" &&
                            EsSimple(p.PropertyType))
                .ToList();
        }

        private static bool EsSimple(Type tipo)
        {
            var t = Nullable.GetUnderlyingType(tipo) ?? tipo;
            return t == typeof(int) || t == typeof(long) || t == typeof(string) ||
                   t == typeof(bool) || t == typeof(DateTime) || t == typeof(decimal);
        }

        private static string Etiqueta(string nombre)
        {
            return Regex.Replace(nombre, "(?<=[a-z])(?=[A-Z])", " ");
        }

        private static string Valor(object fila, PropertyInfo propiedad)
        {
            var valor = propiedad.GetValue(fila);

            // Llave foránea: si su entidad relacionada viene cargada, se muestra su nombre en vez del número
            if (propiedad.Name != "Id" && Nullable.GetUnderlyingType(propiedad.PropertyType) == typeof(int))
            {
                var tipo = fila.GetType();
                var nombre = "_" + propiedad.Name.ToLower();
                var navegacion = tipo.GetProperty(nombre) ?? tipo.GetProperty(nombre + "s");
                var relacionado = navegacion?.GetValue(fila);
                if (relacionado != null)
                {
                    var texto = TextoDe(relacionado);
                    if (!string.IsNullOrEmpty(texto))
                        return texto;
                }
            }

            if (valor == null)
                return "";
            if (valor is bool b)
                return b ? "Sí" : "No";
            if (valor is DateTime d)
                return d.TimeOfDay == TimeSpan.Zero ? d.ToString("dd/MM/yyyy") : d.ToString("dd/MM/yyyy HH:mm");
            return valor.ToString() ?? "";
        }

        // Texto con el que se identifica una entidad relacionada
        private static string? TextoDe(object entidad)
        {
            foreach (var nombre in new[] { "Nombre", "Valor", "Correo" })
            {
                var valor = entidad.GetType().GetProperty(nombre)?.GetValue(entidad);
                if (valor != null)
                    return valor.ToString();
            }

            // Organizadores no tienen nombre propio: se usa el de su persona
            var persona = entidad.GetType().GetProperty("_persona")?.GetValue(entidad);
            return persona != null ? TextoDe(persona) : null;
        }
    }
}
