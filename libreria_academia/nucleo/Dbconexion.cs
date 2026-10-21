
namespace libreria_academia.nucleo
{

    public class Dbconexion
    {
        public static string StringConexion(){
            return "server=localhost\\SQLEXPRESS;database=db_academia;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}
