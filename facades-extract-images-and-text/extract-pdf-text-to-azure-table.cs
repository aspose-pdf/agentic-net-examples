using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Data.Tables (used when the Azure SDK package is not
// referenced). These provide just enough functionality for the sample to
// compile and run in a test environment. In a real project you should add the
// NuGet package "Azure.Data.Tables" instead of using these stubs.
// ---------------------------------------------------------------------------
namespace Azure.Data.Tables
{
    using System.Collections.Generic;

    public class TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public TableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        // Indexer used in the sample: entity["Content"] = extractedText;
        public object this[string key]
        {
            get => _properties.TryGetValue(key, out var value) ? value : null;
            set => _properties[key] = value;
        }
    }

    public class TableClient
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        // Simple in‑memory store to emulate Azure Table storage for demo purposes.
        private static readonly Dictionary<(string Table, string PartitionKey, string RowKey), TableEntity> _store
            = new Dictionary<(string, string, string), TableEntity>();

        public TableClient(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }

        public void CreateIfNotExists()
        {
            // No‑op for the stub – in real SDK this would create the table if missing.
        }

        public void UpsertEntity(TableEntity entity)
        {
            var key = (_tableName, entity.PartitionKey, entity.RowKey);
            _store[key] = entity; // Insert or replace.
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string pdfPath = "input.pdf";

        // Azure Table storage connection details (dummy values for the stub)
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Extract text from the PDF using PdfExtractor (Facades API)
        // -------------------------------------------------
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding (default)
            extractor.ExtractText();

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                extractedText = Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        // -------------------------------------------------
        // Store the extracted text in Azure Table storage
        // -------------------------------------------------
        // Create a TableClient for the target table
        var tableClient = new Azure.Data.Tables.TableClient(storageConnectionString, tableName);
        tableClient.CreateIfNotExists();

        // Use the PDF file name (without extension) as the PartitionKey (document ID)
        string documentId = Path.GetFileNameWithoutExtension(pdfPath);

        // RowKey must be unique; using a GUID ensures uniqueness
        var entity = new Azure.Data.Tables.TableEntity(documentId, Guid.NewGuid().ToString());
        entity["Content"] = extractedText; // store the extracted text

        // Insert or replace the entity in the table
        tableClient.UpsertEntity(entity);

        Console.WriteLine($"Extracted text stored in Azure Table '{tableName}' with PartitionKey='{documentId}'.");
    }
}
