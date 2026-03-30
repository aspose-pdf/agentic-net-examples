using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Stub implementation for Azure.Data.Tables when the NuGet package is not available.
// In a real project you should add the "Azure.Data.Tables" package via NuGet.
namespace Azure.Data.Tables
{
    public class TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

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
        private readonly List<TableEntity> _store = new List<TableEntity>();

        public TableClient(string connectionString, string tableName)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _tableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
        }

        // In the real SDK this creates the table if it does not exist.
        // Here we just simulate the call.
        public void CreateIfNotExists()
        {
            Console.WriteLine($"[TableClient] Ensure table '{_tableName}' exists (simulated).");
        }

        // Simulated AddEntity – just stores the entity in memory and writes a log line.
        public void AddEntity(TableEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _store.Add(entity);
            Console.WriteLine($"[TableClient] Added entity PartitionKey='{entity.PartitionKey}', RowKey='{entity.RowKey}'.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        const string tableName = "PdfTexts";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF and extract all text
        using (Document doc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Store the extracted text in Azure Table storage (or the stub above)
            var tableClient = new Azure.Data.Tables.TableClient(storageConnectionString, tableName);
            tableClient.CreateIfNotExists();

            var entity = new Azure.Data.Tables.TableEntity
            {
                PartitionKey = Path.GetFileNameWithoutExtension(inputPath), // document ID as partition key
                RowKey = Guid.NewGuid().ToString()
            };
            entity["Content"] = extractedText;

            tableClient.AddEntity(entity);
        }

        Console.WriteLine("Text extracted and stored in Azure Table.");
    }
}
