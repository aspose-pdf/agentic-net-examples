using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;          // PdfExtractor resides here

// ---------------------------------------------------------------------------
// Minimal stub implementation for Npgsql when the real NuGet package is not
// referenced. This allows the sample to compile and run (without actual DB
// connectivity) in environments where Npgsql is unavailable.
// ---------------------------------------------------------------------------
namespace Npgsql
{
    // Simple stub for NpgsqlConnection
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() => Console.WriteLine($"[Stub] Opened connection: {_connectionString}");
        public void Close() => Console.WriteLine("[Stub] Closed connection");
        public void Dispose() => Close();
    }

    // Simple stub for NpgsqlCommand
    public class NpgsqlCommand : IDisposable
    {
        private readonly string _commandText;
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlParameterCollection _parameters = new NpgsqlParameterCollection();
        public NpgsqlCommand(string commandText, NpgsqlConnection connection)
        {
            _commandText = commandText;
            _connection = connection;
        }
        public NpgsqlParameterCollection Parameters => _parameters;
        public int ExecuteNonQuery()
        {
            Console.WriteLine("[Stub] Executing SQL: " + _commandText);
            foreach (var p in _parameters)
                Console.WriteLine($"    Parameter {p.Name} = {p.Value}");
            // Return a dummy affected‑rows count
            return 1;
        }
        public void Dispose() { /* no resources to free */ }
    }

    // Simple stub for a parameter collection
    public class NpgsqlParameterCollection : System.Collections.Generic.List<NpgsqlParameter>
    {
        public void AddWithValue(string name, object value) => Add(new NpgsqlParameter(name, value));
    }

    // Simple stub for a parameter object
    public class NpgsqlParameter
    {
        public string Name { get; }
        public object Value { get; }
        public NpgsqlParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}

class PdfTextToPostgres
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // PostgreSQL connection string – adjust host, credentials, and database as needed
        const string connectionString = "Host=localhost;Username=postgres;Password=secret;Database=mydb";

        // Ensure the PDF file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);               // Writes extracted text to the stream
                string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                // Insert the extracted text into PostgreSQL (or the stub above)
                using (Npgsql.NpgsqlConnection connection = new Npgsql.NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Example table: pdf_texts(id SERIAL PRIMARY KEY, file_name TEXT, content TEXT)
                    const string insertSql = "INSERT INTO pdf_texts (file_name, content) VALUES (@file, @content)";

                    using (Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(insertSql, connection))
                    {
                        command.Parameters.AddWithValue("file", Path.GetFileName(pdfPath));
                        command.Parameters.AddWithValue("content", extractedText);
                        command.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("Text extraction and database insertion completed successfully.");
            }
            // No need to call extractor.Close(); the using block disposes it.
        }
    }
}
