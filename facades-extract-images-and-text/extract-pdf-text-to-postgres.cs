using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

// Stub implementation for Npgsql types to allow compilation without the actual NuGet package.
// In a real project, add the Npgsql NuGet package (e.g., <PackageReference Include="Npgsql" Version="*" />)
// and remove these stub definitions.
namespace Npgsql
{
    public class NpgsqlConnection : IDisposable
    {
        private readonly string _connectionString;
        public NpgsqlConnection(string connectionString) => _connectionString = connectionString;
        public void Open() { /* In a real implementation this would open the DB connection */ }
        public void Dispose() { /* Cleanup resources */ }
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
            // Stub: pretend the command succeeded.
            Console.WriteLine($"[Stub] Executing SQL: {_commandText}");
            foreach (var p in Parameters.GetAll())
                Console.WriteLine($"[Stub] Parameter: {p.Name} = {p.Value}");
            return 1;
        }
        public void Dispose() { /* Cleanup */ }
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

    public class NpgsqlParameterCollection
    {
        private readonly System.Collections.Generic.List<NpgsqlParameter> _list = new System.Collections.Generic.List<NpgsqlParameter>();
        public void AddWithValue(string name, object value) => _list.Add(new NpgsqlParameter(name, value));
        internal System.Collections.Generic.IEnumerable<NpgsqlParameter> GetAll() => _list;
    }
}

class PdfTextToPostgres
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists for the demo – create a minimal one in‑memory and save it.
        using (Document seed = new Document())
        {
            // Add a single page with some sample text so that extraction yields content.
            Page page = seed.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample text for extraction."));
            seed.Save(pdfPath);
        }

        // PostgreSQL connection string (adjust parameters as needed)
        const string connString = "Host=localhost;Port=5432;Username=postgres;Password=your_password;Database=your_database";

        // Extract text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Perform the text extraction (Unicode encoding is default)
            extractor.ExtractText();

            // Capture the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);          // Writes the whole document text to the stream
                ms.Position = 0;                // Reset stream position for reading

                using (StreamReader reader = new StreamReader(ms))
                {
                    extractedText = reader.ReadToEnd();   // Retrieve the text as a string
                }
            }
        }

        // Store the extracted text in a PostgreSQL table
        using (Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connString))
        {
            conn.Open();

            // Example table: documents(id UUID PRIMARY KEY, content TEXT)
            using (Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(
                "INSERT INTO documents (id, content) VALUES (@id, @content)", conn))
            {
                cmd.Parameters.AddWithValue("id", Guid.NewGuid());
                cmd.Parameters.AddWithValue("content", extractedText);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Text extraction and database insertion completed successfully.");
    }
}
