using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal stubs for Azure.Data.Tables when the NuGet package is not available.
// These stubs provide just enough functionality for the sample to compile and run.
namespace Azure.Data.Tables
{
    public class TableServiceClient
    {
        private readonly string _connectionString;
        public TableServiceClient(string connectionString) => _connectionString = connectionString;
        public TableClient GetTableClient(string tableName) => new TableClient(tableName);
    }

    public class TableClient
    {
        private readonly string _tableName;
        public TableClient(string tableName) => _tableName = tableName;
        public void CreateIfNotExists() { /* No‑op for stub */ }
        public void AddEntity(TableEntity entity)
        {
            // Safely retrieve the "Content" property – it may be null.
            string content = entity.ContainsKey("Content") && entity["Content"] != null
                ? entity["Content"].ToString()
                : string.Empty;

            // In a real implementation this would send the entity to Azure Table storage.
            // For the stub we simply write to console to demonstrate that the call succeeded.
            Console.WriteLine($"[Stub] Entity added to table '{_tableName}': PartitionKey='{entity.PartitionKey}', RowKey='{entity.RowKey}', Content length={content.Length}");
        }
    }

    public class TableEntity : System.Collections.Generic.Dictionary<string, object>
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public TableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            this["PartitionKey"] = partitionKey;
            this["RowKey"] = rowKey;
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdfPath = "input.pdf";

        // Azure Table storage connection details (stubbed – value not used by the stub implementation)
        const string storageConnectionString = "YourAzureStorageConnectionString";
        const string tableName = "PdfTexts";

        // Ensure a PDF file exists – create a minimal one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (Document placeholder = new Document())
            {
                placeholder.Pages.Add(); // add a blank page
                placeholder.Save(inputPdfPath);
                Console.WriteLine($"[Info] Created placeholder PDF at '{inputPdfPath}'.");
            }
        }

        // Load the PDF document using the recommended Document lifecycle pattern
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Initialize the PdfExtractor facade and bind it to the loaded document
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfDoc);

            // Extract all text from the document (Unicode encoding is default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream textStream = new MemoryStream())
            {
                extractor.GetText(textStream);
                string extractedText = Encoding.UTF8.GetString(textStream.ToArray());

                // Store the extracted text in Azure Table storage (using the stub implementation)
                var serviceClient = new Azure.Data.Tables.TableServiceClient(storageConnectionString);
                var tableClient = serviceClient.GetTableClient(tableName);
                tableClient.CreateIfNotExists();

                // Use the PDF file name (without extension) as the PartitionKey
                string partitionKey = Path.GetFileNameWithoutExtension(inputPdfPath) ?? "unknown";
                // Use a GUID as the RowKey to ensure uniqueness
                string rowKey = Guid.NewGuid().ToString();

                var entity = new Azure.Data.Tables.TableEntity(partitionKey, rowKey)
                {
                    { "Content", extractedText }
                };

                tableClient.AddEntity(entity);
            }

            // Release resources held by the extractor
            extractor.Close();
        }

        Console.WriteLine("Text extraction and storage completed.");
    }
}
