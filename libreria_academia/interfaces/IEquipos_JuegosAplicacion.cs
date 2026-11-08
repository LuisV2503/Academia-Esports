using libreria_academia.entidades;


namespace libreria_academia.interfaces
{
    public interface IEquipos_JuegosAplicacion
    {
        Equipos_Juegos Insertar(Equipos_Juegos entidad);
        Equipos_Juegos Actualizar(Equipos_Juegos entidad);
        List<Equipos_Juegos> Consultar();
        Equipos_Juegos Borrar(Equipos_Juegos entidad);
        List<Equipos_Juegos> Buscar(Equipos_Juegos entidad);
    }
}
