using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Npgsql;

// Minimal stubs for Npgsql types to allow compilation without the actual NuGet package.
// In a real project, reference the official Npgsql package instead of these stubs.
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
        public int ExecuteNonQuery() => 0; // Stub returns 0 rows affected.
        public void Dispose() { /* No‑op stub */ }
    }

    public class NpgsqlParameterCollection
    {
        private readonly System.Collections.Generic.List<NpgsqlParameter> _parameters = new System.Collections.Generic.List<NpgsqlParameter>();
        public void AddWithValue(string name, object value) => _parameters.Add(new NpgsqlParameter(name, value));
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
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists – create a simple one if it does not.
        if (!File.Exists(pdfPath))
        {
            CreateSamplePdf(pdfPath);
        }

        // PostgreSQL connection string (adjust credentials and database name as needed)
        const string connectionString = "Host=localhost;Username=postgres;Password=yourpassword;Database=yourdb";

        // --------------------------------------------------------------------
        // Extract text from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // --------------------------------------------------------------------
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Initialize the extractor with the PDF file
            extractor.BindPdf(pdfPath);

            // Perform text extraction (Unicode encoding is the default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                // Convert the Unicode bytes to a .NET string
                extractedText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }

        // --------------------------------------------------------------------
        // Store the extracted text in a PostgreSQL table
        // --------------------------------------------------------------------
        using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            // Example table: pdf_texts(pdf_name TEXT, content TEXT)
            const string insertSql = "INSERT INTO pdf_texts (pdf_name, content) VALUES (@name, @content)";

            using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, conn))
            {
                cmd.Parameters.AddWithValue("name", Path.GetFileName(pdfPath));
                cmd.Parameters.AddWithValue("content", extractedText);
                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Text extracted from PDF and stored in PostgreSQL successfully.");
    }

    /// <summary>
    /// Creates a minimal PDF containing a single line of text. This method is used only
    /// when the expected input file does not exist, allowing the sample program to run
    /// without external resources.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        // Create a new PDF document.
        Document doc = new Document();
        // Add a page.
        Page page = doc.Pages.Add();
        // Add a text fragment.
        TextFragment tf = new TextFragment("Sample PDF generated for extraction demo.");
        page.Paragraphs.Add(tf);
        // Save the document to the specified path.
        doc.Save(path);
    }
}
