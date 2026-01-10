using Npgsql;
using System;
using FarmaControlPlus;

namespace TuProyecto.Services
{
    public class VentaService
    {
        public void DescontarStock(string nombre, int cantidad)
        {
            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"
            UPDATE medicamentos
            SET stock = stock - @cantidad
            WHERE nombre = @nombre
              AND stock >= @cantidad";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    int filas = cmd.ExecuteNonQuery();
                    if (filas == 0)
                        throw new Exception($"Stock insuficiente para {nombre}");
                }
            }
        }
    }
}