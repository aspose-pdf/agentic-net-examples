using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Azure.Data.Tables;

// -----------------------------------------------------------------------------
// Minimal stubs for Azure.Data.Tables when the NuGet package is not referenced.
// Remove these stubs and add a reference to the official Azure.Data.Tables package
// for production use.
// -----------------------------------------------------------------------------
namespace Azure.Data.Tables
{
    // Simple representation of a table entity.
    public class TableEntity : System.Collections.Generic.Dictionary<string, object>
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public TableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
    }

    // Stub for TableClient – only the members used in this sample are implemented.
    public class TableClient
    {
        private readonly string _tableName;
        public TableClient(string tableName) => _tableName = tableName;

        public void CreateIfNotExists()
        {
            // No‑op stub – in real code this creates the table if it does not exist.
            Console.WriteLine($"[Stub] Ensure table '{_tableName}' exists.");
        }

        public void AddEntity(TableEntity entity)
        {
            // No‑op stub – in real code this inserts the entity into Azure Table storage.
            Console.WriteLine($"[Stub] Added entity to table '{_tableName}': PartitionKey='{entity.PartitionKey}', RowKey='{entity.RowKey}'.");
        }
    }

    // Stub for TableServiceClient – only the members used in this sample are implemented.
    public class TableServiceClient
    {
        private readonly string _connectionString;
        public TableServiceClient(string connectionString) => _connectionString = connectionString;

        public TableClient GetTableClient(string tableName) => new TableClient(tableName);
    }
}

class Program
{
    static void Main()
    {
        // Input PDF file path – change as needed or keep the default for the demo.
        const string pdfPath = "input.pdf";

        // Azure Table storage connection details – replace with real values for production.
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT_NAME;AccountKey=YOUR_ACCOUNT_KEY;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";

        // Ensure the PDF exists – if not, create a simple one on‑the‑fly so the sample runs without external files.
        EnsureSamplePdfExists(pdfPath);

        // Use the file name (without extension) as the document ID (partition key).
        string documentId = Path.GetFileNameWithoutExtension(pdfPath);

        // Extract text from the PDF using PdfExtractor (Facades API).
        string extractedText = ExtractTextFromPdf(pdfPath);

        // Store the extracted text in Azure Table storage.
        StoreTextInAzureTable(storageConnectionString, tableName, documentId, extractedText);

        Console.WriteLine("Text extraction and storage completed.");
    }

    // Creates a minimal PDF with a single page of sample text if the target file does not exist.
    private static void EnsureSamplePdfExists(string path)
    {
        if (File.Exists(path))
            return;

        Console.WriteLine($"[Info] '{path}' not found – creating a sample PDF.");
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample PDF generated for text extraction demo."));
            doc.Save(path);
        }
    }

    // Extracts all text from a PDF file and returns it as a string.
    private static string ExtractTextFromPdf(string pdfFilePath)
    {
        // PdfExtractor implements IDisposable, so use a using block for deterministic disposal.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfFilePath);

            // Perform text extraction.
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream.
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                // Convert the stream bytes to a UTF‑8 string.
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }

    // Inserts a new entity containing the extracted text into Azure Table storage.
    private static void StoreTextInAzureTable(string connectionString, string tableName, string partitionKey, string textContent)
    {
        // Create a TableServiceClient to interact with the storage account.
        TableServiceClient serviceClient = new TableServiceClient(connectionString);

        // Get a reference to the specific table (creates it if it does not exist).
        TableClient tableClient = serviceClient.GetTableClient(tableName);
        tableClient.CreateIfNotExists();

        // Create a TableEntity with PartitionKey = document ID and a unique RowKey.
        TableEntity entity = new TableEntity(partitionKey, Guid.NewGuid().ToString())
        {
            { "Content", textContent }
        };

        // Insert the entity into the table.
        tableClient.AddEntity(entity);
    }
}
