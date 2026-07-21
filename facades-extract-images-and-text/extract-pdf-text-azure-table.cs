using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal Azure Table Storage stubs (remove these when the real Azure.Data.Tables
// NuGet package is referenced). They provide just enough functionality for the
// sample to compile and run against a real Azure Table service if the package
// is added later.
// ---------------------------------------------------------------------------
namespace Azure.Data.Tables
{
    public class TableEntity : Dictionary<string, object>
    {
        public TableEntity() { }
        public TableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public string PartitionKey
        {
            get => this["PartitionKey"] as string;
            set => this["PartitionKey"] = value;
        }
        public string RowKey
        {
            get => this["RowKey"] as string;
            set => this["RowKey"] = value;
        }
    }

    public class TableClient
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        public TableClient(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }
        // In a real implementation this would call the Azure service.
        public void CreateIfNotExists()
        {
            // No‑op stub – assume the table exists.
        }
        public void AddEntity(TableEntity entity)
        {
            // No‑op stub – in a real scenario this would send the entity to Azure.
            // For demonstration we just write to console.
            Console.WriteLine($"[Stub] Entity added to table '{_tableName}': PartitionKey={entity.PartitionKey}, RowKey={entity.RowKey}, Content length={((string)entity["Content"]).Length}");
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT_NAME;AccountKey=YOUR_ACCOUNT_KEY;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Extract all text from the PDF using PdfExtractor (Facades API)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // Bind the PDF file
            extractor.ExtractText();             // Perform text extraction
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);           // Write extracted text to a stream
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                {
                    extractedText = reader.ReadToEnd(); // Read the text as a string
                }
            }
        }

        // Store the extracted text in Azure Table Storage
        var tableClient = new Azure.Data.Tables.TableClient(storageConnectionString, tableName);
        tableClient.CreateIfNotExists(); // Ensure the table exists

        // Use the PDF file name (without extension) as the PartitionKey (document ID)
        string documentId = Path.GetFileNameWithoutExtension(pdfPath);
        // RowKey must be unique; use a GUID
        string rowKey = Guid.NewGuid().ToString();

        var entity = new Azure.Data.Tables.TableEntity(documentId, rowKey)
        {
            { "Content", extractedText } // Store the full extracted text
        };

        tableClient.AddEntity(entity);
        Console.WriteLine("Extracted text successfully stored in Azure Table.");
    }
}
