using System;
using System.IO;
using Aspose.Pdf.Facades;
using Npgsql; // Added using for Npgsql (stub defined below)

// ---------------------------------------------------------------------------
// Minimal stub implementation for the Npgsql library (PostgreSQL ADO.NET provider)
// ---------------------------------------------------------------------------
namespace Npgsql
{
    // Simple stub for NpgsqlConnection that implements IDisposable.
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() { /* No‑op stub – in real code this would open the DB connection */ }
        public void Close() { /* No‑op stub */ }
        public void Dispose() => Close();
    }

    // Simple stub for NpgsqlCommand that implements IDisposable.
    public class NpgsqlCommand : IDisposable
    {
        private readonly string _commandText;
        private readonly NpgsqlConnection _connection;
        public NpgsqlCommand(string commandText, NpgsqlConnection connection)
        {
            _commandText = commandText;
            _connection = connection;
            Parameters = new NpgsqlParameterCollection();
        }
        public NpgsqlParameterCollection Parameters { get; }
        // In a real implementation this would execute the SQL against PostgreSQL.
        // Here we simply return 0 to indicate success.
        public int ExecuteNonQuery() => 0;
        public void Dispose() { /* No‑op stub */ }
    }

    // Stub collection that mimics the AddWithValue method used in the sample.
    public class NpgsqlParameterCollection
    {
        public void AddWithValue(string parameterName, object value)
        {
            // No‑op stub – parameters are ignored in this mock implementation.
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // PostgreSQL connection string (replace placeholders with real values)
        const string connectionString = "Host=localhost;Username=postgres;Password=your_password;Database=your_database";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ---------- Extract text using Aspose.Pdf.Facades ----------
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(inputPdfPath);          // Load the PDF
            extractor.ExtractText();                  // Perform text extraction

            // GetText requires a destination stream. Use a MemoryStream and read the text.
            string extractedText;
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream); // write extracted text to the stream
                textStream.Position = 0;        // rewind for reading
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }

            // ---------- Store extracted text in PostgreSQL ----------
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                const string insertSql = @"
                    INSERT INTO pdf_documents (document_name, content)
                    VALUES (@name, @content)";

                using (NpgsqlCommand command = new NpgsqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("name", Path.GetFileName(inputPdfPath));
                    command.Parameters.AddWithValue("content", extractedText);
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Text extracted from PDF and stored in PostgreSQL successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
