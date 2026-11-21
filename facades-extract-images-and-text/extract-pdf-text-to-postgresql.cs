using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;          // PdfExtractor resides here

// ---------------------------------------------------------------------------
// Minimal stub implementation of the Npgsql ADO.NET provider.
// This allows the sample to compile without adding the real NuGet package.
// In production replace these stubs with the official Npgsql package.
// ---------------------------------------------------------------------------
namespace Npgsql
{
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() { /* No‑op stub – real implementation opens a PostgreSQL connection */ }
        public void Dispose() { /* No‑op */ }
    }

    public class NpgsqlParameter
    {
        public string ParameterName { get; }
        public object Value { get; }
        public NpgsqlParameter(string name, object value)
        {
            ParameterName = name;
            Value = value;
        }
    }

    public class NpgsqlParameterCollection : List<NpgsqlParameter>
    {
        public void AddWithValue(string name, object value) => Add(new NpgsqlParameter(name, value));
    }

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
        public int ExecuteNonQuery()
        {
            // Stub implementation – in a real scenario this would execute the SQL.
            // Returning 0 indicates "no rows affected" which is sufficient for the demo.
            return 0;
        }
        public void Dispose() { /* No‑op */ }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=your_password;Database=your_db";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------------------------
        // Extract text from the PDF using PdfExtractor (Facades API)
        // -------------------------------------------------------------------
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractText();

            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                textStream.Position = 0;
                using (StreamReader reader = new StreamReader(textStream))
                {
                    extractedText = reader.ReadToEnd();
                }
            }
        }

        // -------------------------------------------------------------------
        // Store the extracted text in PostgreSQL (stubbed Npgsql implementation)
        // -------------------------------------------------------------------
        using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connectionString))
        {
            conn.Open();

            const string insertSql = "INSERT INTO pdf_documents (content) VALUES (@content)";
            using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("@content", extractedText ?? string.Empty);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Text extraction and database insertion completed successfully.");
    }
}
