using libreria_academia.entidades;
using libreria_academia.interfaces;
using Microsoft.EntityFrameworkCore;

namespace libreria_academia.implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Personas <-> Sueldos (1 a 1)
            modelBuilder.Entity<Personas>()
                .HasOne(p => p._sueldo)
                .WithOne(s => s._persona)
                .HasForeignKey<Personas>(p => p.Sueldo);

            // Personas <-> roles (muchos a 1)
            modelBuilder.Entity<Jugadores>()
                .HasOne(j => j._persona)
                .WithMany(p => p.Jugadores)
                .HasForeignKey(j => j.Persona);

            modelBuilder.Entity<Entrenadores>()
                .HasOne(e => e._persona)
                .WithMany(p => p.Entrenadores)
                .HasForeignKey(e => e.Persona);

            modelBuilder.Entity<Analistas>()
                .HasOne(a => a._persona)
                .WithMany(p => p.Analistas)
                .HasForeignKey(a => a.Persona);

            modelBuilder.Entity<Psicologos>()
                .HasOne(ps => ps._persona)
                .WithMany(p => p.Psicologos)
                .HasForeignKey(ps => ps.Persona);

            modelBuilder.Entity<Fisioterapeutas>()
                .HasOne(f => f._persona)
                .WithMany(p => p.Fisioterapeutas)
                .HasForeignKey(f => f.Persona);

            modelBuilder.Entity<Organizadores>()
                .HasOne(o => o._persona)
                .WithMany(p => p.Organizadores)
                .HasForeignKey(o => o.Persona);

            // Equipos <-> roles (muchos a 1)
            modelBuilder.Entity<Jugadores>()
                .HasOne(j => j._equipo)
                .WithMany(e => e.Jugadores)
                .HasForeignKey(j => j.Equipo);

            modelBuilder.Entity<Entrenadores>()
                .HasOne(e => e._equipo)
                .WithMany(eq => eq.Entrenadores)
                .HasForeignKey(e => e.Equipo);

            modelBuilder.Entity<Analistas>()
                .HasOne(a => a._equipo)
                .WithMany(e => e.Analistas)
                .HasForeignKey(a => a.Equipo);

            modelBuilder.Entity<Psicologos>()
                .HasOne(ps => ps._equipo)
                .WithMany(e => e.Psicologos)
                .HasForeignKey(ps => ps.Equipo);

            modelBuilder.Entity<Fisioterapeutas>()
                .HasOne(f => f._equipo)
                .WithMany(e => e.Fisioterapeutas)
                .HasForeignKey(f => f.Equipo);

            // Equipos_Juegos
            modelBuilder.Entity<Equipos_Juegos>()
                .HasOne(ej => ej._equipos)
                .WithMany(e => e.EquiposJuegos)
                .HasForeignKey(ej => ej.Equipo);

            modelBuilder.Entity<Equipos_Juegos>()
                .HasOne(ej => ej._juegos)
                .WithMany(j => j.EquiposJuegos)
                .HasForeignKey(ej => ej.Juego);

            // Equipos_Empresas
            modelBuilder.Entity<Equipos_Empresas>()
                .HasOne(ee => ee._equipos)
                .WithMany(e => e.EquiposEmpresas)
                .HasForeignKey(ee => ee.Equipo);

            modelBuilder.Entity<Equipos_Empresas>()
                .HasOne(ee => ee._empresas)
                .WithMany(e => e.EquiposEmpresas)
                .HasForeignKey(ee => ee.Empresa);

            // Empresas <-> Tipo
            modelBuilder.Entity<Empresas>()
                .HasOne(e => e._tipo)
                .WithMany(t => t.Empresas)
                .HasForeignKey(e => e.Tipo);

            // Equipos_Academias
            modelBuilder.Entity<Equipos_Academias>()
                .HasOne(ea => ea._equipos)
                .WithMany(e => e.EquiposAcademias)
                .HasForeignKey(ea => ea.Equipo);

            modelBuilder.Entity<Equipos_Academias>()
                .HasOne(ea => ea._academias)
                .WithMany(a => a.EquiposAcademias)
                .HasForeignKey(ea => ea.Academia);

            // Empresas_Academias
            modelBuilder.Entity<Empresas_Academias>()
                .HasOne(ea => ea._empresas)
                .WithMany(e => e.EmpresasAcademias)
                .HasForeignKey(ea => ea.Empresa);

            modelBuilder.Entity<Empresas_Academias>()
                .HasOne(ea => ea._academias)
                .WithMany(a => a.EmpresasAcademias)
                .HasForeignKey(ea => ea.Academia);

            // Empresas_Academias_Eventos
            modelBuilder.Entity<Empresas_Academias_Eventos>()
                .HasOne(x => x._empresas)
                .WithMany(e => e.EmpresasAcademiasEventos)
                .HasForeignKey(x => x.Empresa);

            modelBuilder.Entity<Empresas_Academias_Eventos>()
                .HasOne(x => x._academias)
                .WithMany(a => a.EmpresasAcademiasEventos)
                .HasForeignKey(x => x.Academia);

            modelBuilder.Entity<Empresas_Academias_Eventos>()
                .HasOne(x => x._eventos)
                .WithMany(ev => ev.EmpresasAcademiasEventos)
                .HasForeignKey(x => x.Evento);

            // Eventos_Organizadores
            modelBuilder.Entity<Eventos_Organizadores>()
                .HasOne(eo => eo._eventos)
                .WithMany(e => e.EventosOrganizadores)
                .HasForeignKey(eo => eo.Evento);

            modelBuilder.Entity<Eventos_Organizadores>()
                .HasOne(eo => eo._organizadores)
                .WithMany(o => o.EventosOrganizadores)
                .HasForeignKey(eo => eo.Organizador);

            // Entretenimientos
            modelBuilder.Entity<Entretenimientos>()
                .HasOne(e => e._eventos)
                .WithMany(ev => ev.Entretenimientos)
                .HasForeignKey(e => e.Evento);
        }

        public DbSet<Sueldos>? Sueldos { get; set; }
        public DbSet<Personas>? Personas { get; set; }
        public DbSet<Jugadores>? Jugadores { get; set; }
        public DbSet<Entrenadores>? Entrenadores { get; set; }
        public DbSet<Analistas>? Analistas { get; set; }
        public DbSet<Psicologos>? Psicologos { get; set; }
        public DbSet<Fisioterapeutas>? Fisioterapeutas { get; set; }
        public DbSet<Organizadores>? Organizadores { get; set; }
        public DbSet<Equipos>? Equipos { get; set; }
        public DbSet<Juegos>? Juegos { get; set; }
        public DbSet<Equipos_Juegos>? Equipos_Juegos { get; set; }
        public DbSet<Equipos_Empresas>? Equipos_Empresas { get; set; }
        public DbSet<Empresas>? Empresas { get; set; }
        public DbSet<Tipo>? Tipo { get; set; }
        public DbSet<Equipos_Academias>? Equipos_Academias { get; set; }
        public DbSet<Academias>? Academias { get; set; }
        public DbSet<Empresas_Academias>? Empresas_Academias { get; set; }
        public DbSet<Empresas_Academias_Eventos>? Empresas_Academias_Eventos { get; set; }
        public DbSet<Eventos>? Eventos { get; set; }
        public DbSet<Eventos_Organizadores>? Eventos_Organizadores { get; set; }
        public DbSet<Entretenimientos>? Entretenimientos { get; set; }
    }
}