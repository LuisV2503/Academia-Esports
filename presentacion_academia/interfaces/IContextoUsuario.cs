namespace presentacion_academia.interfaces
{
    // Guarda, durante una petición del front, el id del usuario con sesión iniciada
    public interface IContextoUsuario
    {
        int? Usuario { get; set; }
    }
}
