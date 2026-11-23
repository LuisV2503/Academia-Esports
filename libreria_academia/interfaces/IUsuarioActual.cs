namespace libreria_academia.interfaces
{
    // Quién está haciendo la petición (lo implementa el proyecto de la API)
    public interface IUsuarioActual
    {
        int? Id { get; }
    }
}
