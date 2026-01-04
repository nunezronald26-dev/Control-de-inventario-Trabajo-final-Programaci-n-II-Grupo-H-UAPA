using Npgsql;

namespace FarmaControlPlus
{
    public static class ConexionBD
    {
        private static string cadena =
            "Host=localhost;Port=5432;Database=FarmaControlPlus;Username=postgres;Password=admin1234";

        public static NpgsqlConnection ObtenerConexion()
        {
            return new NpgsqlConnection(cadena);
        }
    }
}
