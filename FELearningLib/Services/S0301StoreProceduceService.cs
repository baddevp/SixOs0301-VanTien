using Microsoft.Data.SqlClient;
using System.Text;

namespace FELearningLib.Services
{
    public class S0301StoreProceduceService
    {
        private readonly IConfiguration _configuration;

        public S0301StoreProceduceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateDynamicStoredProcedure(string spName, List<string> propertyNames)
        {
            var connectionString = _configuration.GetConnectionString("Connection");
            var selectClause = new StringBuilder();

            for (int i = 0; i < propertyNames.Count; i++)
            {
                selectClause.Append($"'' AS {propertyNames[i]}");
                if (i < propertyNames.Count - 1)
                {
                    selectClause.Append(", ");
                }
            }

            var sql = $@"
            IF OBJECT_ID('{spName}', 'P') IS NOT NULL
                DROP PROCEDURE {spName};
            GO
            CREATE PROCEDURE {spName}
            AS
            BEGIN
                SELECT {selectClause.ToString()}
            END;
        ";

            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                // Tách các lệnh SQL bằng GO
                var commands = sql.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var commandText in commands)
                {
                    if (!string.IsNullOrWhiteSpace(commandText))
                    {
                        await using var command = new SqlCommand(commandText, connection);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return $"Store procedure '{spName}' đã được tạo thành công.";
            }
            catch (Exception ex)
            {
                return $"Lỗi khi tạo store procedure: {ex.Message}";
            }
        }
    }
}
