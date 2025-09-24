using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;



namespace FELearningLib.Services
{
    public class S0301DynamicClassGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<S0301DynamicClassGeneratorService> _logger;
        public S0301DynamicClassGeneratorService(IConfiguration configuration, ILogger<S0301DynamicClassGeneratorService> logger)
        {
            _configuration = configuration;
            _logger = logger;

        }
        public async Task<string> GenerateClassFileFromStoredProcedure(string spName, string solutionDirectory, string solutionName)
        {
            var connectionString = _configuration.GetConnectionString("Connection");

            try
            {
                var className = Regex.Replace(spName, "^[^_]+_", "", RegexOptions.IgnoreCase);

                if (string.IsNullOrEmpty(className))
                {
                    return "Tên store procedure không hợp lệ.";
                }

                var outputDirectory = Path.Combine(solutionDirectory, "Models");
                var outputFile = Path.Combine(outputDirectory, $"{className}.cs");

                await using var connection = new SqlConnection(connectionString);
                await using var command = new SqlCommand(spName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                var schemaTable = reader.GetSchemaTable();
                if (schemaTable == null) return "Không tìm thấy metadata cho store procedure.";
                var codeBuilder = new StringBuilder();

                codeBuilder.AppendLine("using System;");
                codeBuilder.AppendLine();

                codeBuilder.AppendLine($"namespace {solutionName}.Models");
                codeBuilder.AppendLine("{");
                codeBuilder.AppendLine($"    public class {className}");
                codeBuilder.AppendLine("    {");

                foreach (System.Data.DataRow row in schemaTable.Rows)
                {
                    var columnName = row["ColumnName"].ToString();
                    codeBuilder.AppendLine($"        public string {columnName} {{ get; set; }}");
                }

                codeBuilder.AppendLine("    }");
                codeBuilder.AppendLine("}");

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                await File.WriteAllTextAsync(outputFile, codeBuilder.ToString());

                return $"{outputFile}";
            }

            catch (Exception ex)
            {
                return $"Lỗi khi tạo file class: {ex.Message}";
            }
        }
    }
}
