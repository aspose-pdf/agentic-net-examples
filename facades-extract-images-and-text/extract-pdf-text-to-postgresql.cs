using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // <-- added namespace for TextFragment

// ---------------------------------------------------------------------------
// Stub implementation for Npgsql (PostgreSQL ADO.NET provider).
// This allows the sample to compile without adding the real NuGet package.
// In a production environment replace this stub with the official
// Npgsql package (install‑package Npgsql) and remove the stub namespace.
// ---------------------------------------------------------------------------
namespace Npgsql
{
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() { /* No‑op stub – in real code opens a DB connection */ }
        public void Dispose() { /* No‑op stub */ }
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
            // Stub behaviour – just display what would be executed.
            Console.WriteLine("[Stub Npgsql] Executing SQL: " + _commandText);
            foreach (var p in Parameters)
                Console.WriteLine($"    Parameter: {p.Name} = {p.Value}");
            return 0; // indicate success
        }
        public void Dispose() { /* No‑op stub */ }
    }

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

    public class NpgsqlParameterCollection : System.Collections.Generic.List<NpgsqlParameter>
    {
        public void AddWithValue(string parameterName, object value) => Add(new NpgsqlParameter(parameterName, value));
    }
}

class Program
{
    static void Main()
    {
        const string connectionString = "Host=localhost;Username=postgres;Password=secret;Database=mydb";

        // -------------------------------------------------------------------
        // 1. Create a simple PDF in memory (so we don't depend on an external file)
        // -------------------------------------------------------------------
        byte[] pdfBytes;
        using (var pdfDoc = new Document())
        {
            // Add a single page with some sample text
            var page = pdfDoc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Hello Aspose PDF! This text will be indexed."));
            using (var ms = new MemoryStream())
            {
                pdfDoc.Save(ms, SaveFormat.Pdf);
                pdfBytes = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------
        // 2. Extract all text from the PDF using PdfExtractor (stream overload)
        // ---------------------------------------------------------------
        string extractedText;
        using (var pdfStream = new MemoryStream(pdfBytes))
        using (var extractor = new PdfExtractor())
        {
            // Bind to the in‑memory stream instead of a file path
            extractor.BindPdf(pdfStream);
            extractor.ExtractText(); // Unicode extraction
            using (var textMs = new MemoryStream())
            {
                extractor.GetText(textMs);
                extractedText = Encoding.Unicode.GetString(textMs.ToArray());
            }
        }

        // ---------------------------------------------------------------
        // 3. Store the extracted text in PostgreSQL (stubbed for demo only)
        // ---------------------------------------------------------------
        using (var conn = new Npgsql.NpgsqlConnection(connectionString))
        {
            conn.Open();
            const string sql = "INSERT INTO pdf_documents (file_name, content) VALUES (@name, @content)";
            using (var cmd = new Npgsql.NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("name", "generated.pdf");
                cmd.Parameters.AddWithValue("content", extractedText);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Text extracted and stored successfully.");
    }
}
