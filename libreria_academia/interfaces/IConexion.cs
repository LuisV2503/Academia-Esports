using libreria_academia.entidades;
using Microsoft.EntityFrameworkCore;

namespace libreria_academia.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

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
