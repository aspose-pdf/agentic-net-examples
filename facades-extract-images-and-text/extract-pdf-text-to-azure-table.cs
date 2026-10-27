using System;
using System.IO;
using System.Text;
using Aspose.Pdf.Facades;
using Azure.Data.Tables;

// -----------------------------------------------------------------------------
// Minimal stubs for Azure.Data.Tables when the NuGet package is not available.
// These provide just enough members to compile the sample code.
// -----------------------------------------------------------------------------
namespace Azure.Data.Tables
{
    public class TableServiceClient
    {
        private readonly string _connectionString;
        public TableServiceClient(string connectionString)
        {
            _connectionString = connectionString;
        }
        public TableClient GetTableClient(string tableName) => new TableClient(_connectionString, tableName);
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
        public void CreateIfNotExists() { /* No‑op stub */ }
        public void AddEntity(TableEntity entity) { /* No‑op stub */ }
    }

    public class TableEntity : System.Collections.Generic.Dictionary<string, object>
    {
        public TableEntity(string partitionKey, string rowKey) : base()
        {
            this["PartitionKey"] = partitionKey;
            this["RowKey"] = rowKey;
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";
        const string documentId = "doc123"; // PartitionKey for the Azure Table entity

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Extract all text from the PDF using the Facades API (PdfExtractor)
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractText();

            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                extractedText = Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        // Initialize Azure Table storage client
        TableServiceClient serviceClient = new TableServiceClient(storageConnectionString);
        TableClient tableClient = serviceClient.GetTableClient(tableName);
        tableClient.CreateIfNotExists();

        // Create a table entity: PartitionKey = documentId, RowKey = unique identifier
        TableEntity entity = new TableEntity(documentId, Guid.NewGuid().ToString())
        {
            { "Content", extractedText }
        };

        // Insert the entity into the table
        tableClient.AddEntity(entity);

        Console.WriteLine("Text extracted from PDF and stored in Azure Table.");
    }
}
