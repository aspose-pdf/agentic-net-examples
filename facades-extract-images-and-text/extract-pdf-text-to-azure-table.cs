using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal Azure Table storage stubs – replace with the real Azure.Data.Tables
// package (Azure.Data.Tables) in production. They are provided only to make the
// sample compile when the NuGet package is not referenced.
// ---------------------------------------------------------------------------
namespace Azure.Data.Tables
{
    // Simple representation of a table entity – stores PartitionKey, RowKey and
    // any additional properties in a dictionary.
    public class TableEntity : System.Collections.Generic.Dictionary<string, object>
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public TableEntity(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
            // Store the keys also in the dictionary so that they are persisted
            // when the real SDK is used.
            this["PartitionKey"] = partitionKey;
            this["RowKey"] = rowKey;
        }
    }

    // Stub for TableClient – in a real scenario this talks to Azure Table
    // storage. Here it just writes to the console for demonstration.
    public class TableClient
    {
        private readonly string _tableName;
        public TableClient(string tableName) => _tableName = tableName;

        public void CreateIfNotExists()
        {
            Console.WriteLine($"[Stub] Ensuring table '{_tableName}' exists.");
        }

        public void AddEntity(TableEntity entity)
        {
            Console.WriteLine($"[Stub] Adding entity to table '{_tableName}': PartitionKey='{entity.PartitionKey}', RowKey='{entity.RowKey}'.");
            // In a real implementation the entity would be sent to Azure.
        }
    }

    // Stub for TableServiceClient – creates TableClient instances.
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
        // Input PDF file path
        const string pdfPath = "input.pdf";

        // Azure Table storage connection details (replace with real values when using the real SDK)
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT_NAME;AccountKey=YOUR_ACCOUNT_KEY;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // Extract text from PDF using Aspose.Pdf.Facades
        // -------------------------------------------------
        string extractedText;
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Extract text (Unicode encoding is default)
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
        // Create Table service and client (stub or real SDK)
        Azure.Data.Tables.TableServiceClient serviceClient = new Azure.Data.Tables.TableServiceClient(storageConnectionString);
        Azure.Data.Tables.TableClient tableClient = serviceClient.GetTableClient(tableName);
        tableClient.CreateIfNotExists();

        // Use the PDF file name (without extension) as the partition key
        string documentId = Path.GetFileNameWithoutExtension(pdfPath);

        // Create a new table entity with a unique row key
        Azure.Data.Tables.TableEntity entity = new Azure.Data.Tables.TableEntity(documentId, Guid.NewGuid().ToString())
        {
            { "Content", extractedText }
        };

        // Insert the entity into the table
        tableClient.AddEntity(entity);

        Console.WriteLine("Text extracted and stored in Azure Table successfully.");
    }
}
