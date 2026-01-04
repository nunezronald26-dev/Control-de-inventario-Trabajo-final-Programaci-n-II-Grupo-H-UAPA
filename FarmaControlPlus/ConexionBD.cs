using Npgsql;

namespace FarmaControlPlus
{
    public static class ConexionBD
    {
        private static string cadena =
            "Host=localhost;Port=5432;Database=FarmaControlPlus;Username=postgres;Password=admin";

        public static NpgsqlConnection ObtenerConexion()
        {
            return new NpgsqlConnection(cadena);
        }
    }
}
