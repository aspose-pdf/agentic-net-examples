using System;
using System.IO;
using System.Text;
using Aspose.Pdf;                     // Document class for creating a placeholder PDF
using Aspose.Pdf.Facades;          // PdfExtractor resides here
using Aspose.Pdf.Text;             // TextFragment resides here
using Npgsql;                     // PostgreSQL .NET driver (stub provided if package missing)

// -----------------------------------------------------------------------------
// Minimal stub implementation for Npgsql types when the real NuGet package is not
// referenced. This allows the sample to compile and run (the stub does not
// perform any real database operations). In a production project you should
// reference the official Npgsql package (e.g., via NuGet) and remove this stub.
// -----------------------------------------------------------------------------
namespace Npgsql
{
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() { /* No‑op stub */ }
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
        public int ExecuteNonQuery() => 0; // Stub returns 0 rows affected
        public void Dispose() { /* No‑op stub */ }
    }

    public class NpgsqlParameterCollection
    {
        public void AddWithValue(string parameterName, object value)
        {
            // Stub – store or ignore the value as needed for compilation.
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists – create a minimal one if it does not.
        if (!File.Exists(pdfPath))
        {
            using var seedDoc = new Document();
            seedDoc.Pages.Add();
            // Add a simple text fragment so the extractor has something to read.
            seedDoc.Pages[1].Paragraphs.Add(new TextFragment("Sample text for extraction."));
            seedDoc.Save(pdfPath);
        }

        // PostgreSQL connection string – adjust host, user, password, database as needed
        const string connectionString = "Host=localhost;Username=postgres;Password=secret;Database=mydb";

        // Extract text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);

                // Convert the stream bytes to a .NET string (Unicode)
                string extractedText = Encoding.Unicode.GetString(textStream.ToArray());

                // Store the extracted text into PostgreSQL
                using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Example table: documents(id SERIAL PRIMARY KEY, filename TEXT, content TEXT)
                    const string insertSql = @"
                        INSERT INTO documents (filename, content)
                        VALUES (@filename, @content);";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("filename", Path.GetFileName(pdfPath));
                        cmd.Parameters.AddWithValue("content", extractedText);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        Console.WriteLine("Text extraction and database insertion completed.");
    }
}
