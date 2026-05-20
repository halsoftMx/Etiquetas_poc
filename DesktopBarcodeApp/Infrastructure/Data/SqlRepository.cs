using DesktopBarcodeApp.Domain.Models;
using Microsoft.Data.SqlClient;

namespace DesktopBarcodeApp.Infrastructure.Data
{
    public class SqlRepository
    {
        private readonly string _connectionString;

        public SqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Guardar(EtiquetaModel model)
        {
            using SqlConnection cn = new SqlConnection(_connectionString);

            cn.Open();

            string sql = @"
            INSERT INTO Etiquetas
            (
                  Empresa
                , Direccion
                , Ciudad
                , Producto
                , Composicion
                , Origen
                , Codigo
                , CodigoInterno
                , Metros
            )
            VALUES
            (                
                  @Empresa
                , @Direccion
                , @Ciudad
                , @Producto
                , @Composicion
                , @Origen
                , @Codigo
                , @CodigoInterno
                , @Metros
             )";

            using SqlCommand cmd = new SqlCommand(sql, cn);

            cmd.Parameters.AddWithValue("@Empresa", model.Empresa);
            cmd.Parameters.AddWithValue("@Direccion", model.Direccion);
            cmd.Parameters.AddWithValue("@Ciudad", model.Ciudad);
            cmd.Parameters.AddWithValue("@Producto", model.Producto);
            cmd.Parameters.AddWithValue("@Composicion", model.Composicion);
            cmd.Parameters.AddWithValue("@Origen", model.Origen);
            cmd.Parameters.AddWithValue("@Codigo", model.Codigo);
            cmd.Parameters.AddWithValue("@CodigoInterno", model.CodigoInterno);
            cmd.Parameters.AddWithValue("@Metros", model.Metros);

            cmd.ExecuteNonQuery();
        }
    }
}