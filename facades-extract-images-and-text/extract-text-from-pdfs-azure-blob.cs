using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs – replace with the real NuGet package
// (Azure.Storage.Blobs) in production. These stubs provide just enough API to
// compile the sample and allow a simple in‑memory demonstration.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobServiceClient
    {
        private readonly string _connectionString;
        public BlobServiceClient(string connectionString) => _connectionString = connectionString;
        public BlobContainerClient GetBlobContainerClient(string containerName) => new BlobContainerClient(containerName);
    }

    public class BlobContainerClient
    {
        private readonly string _containerName;
        // In‑memory storage: container -> (blob name -> bytes)
        // Made internal so that other stub classes can access it.
        internal static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> _store
            = new ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>>();

        public BlobContainerClient(string containerName)
        {
            _containerName = containerName;
            _store.GetOrAdd(_containerName, _ => new ConcurrentDictionary<string, byte[]>());
        }

        public BlobClient GetBlobClient(string blobName) => new BlobClient(_containerName, blobName);

        public async Task CreateIfNotExistsAsync()
        {
            // No‑op for the stub – the dictionary is created lazily.
            await Task.CompletedTask;
        }

        public async IAsyncEnumerable<BlobItem> GetBlobsAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var container = _store[_containerName];
            foreach (var kvp in container)
            {
                // Simulate a small delay to mimic I/O.
                await Task.Yield();
                yield return new BlobItem(kvp.Key);
            }
        }
    }

    public class BlobClient
    {
        private readonly string _containerName;
        private readonly string _blobName;
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, byte[]>> Store => BlobContainerClient._store;

        public BlobClient(string containerName, string blobName)
        {
            _containerName = containerName;
            _blobName = blobName;
        }

        public async Task DownloadToAsync(Stream destination)
        {
            var container = Store[_containerName];
            if (container.TryGetValue(_blobName, out var data))
            {
                await destination.WriteAsync(data, 0, data.Length);
                destination.Position = 0;
            }
            else
            {
                // Blob not found – simulate empty stream.
                destination.SetLength(0);
                destination.Position = 0;
            }
        }

        public async Task UploadAsync(Stream source, bool overwrite = false)
        {
            var container = Store[_containerName];
            using var ms = new MemoryStream();
            await source.CopyToAsync(ms);
            var bytes = ms.ToArray();
            if (overwrite)
                container[_blobName] = bytes;
            else
                container.TryAdd(_blobName, bytes);
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; }
        public BlobItem(string name) => Name = name;
    }
}

class Program
{
    // Entry point – async to allow awaiting blob operations.
    static async Task Main(string[] args)
    {
        // Azure Blob Storage connection string (set in environment or replace with your value).
        string connectionString = Environment.GetEnvironmentVariable("AZURE_BLOB_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.Error.WriteLine("Please set the AZURE_BLOB_CONNECTION_STRING environment variable.");
            return;
        }

        // Name of the container that holds the PDF files.
        const string containerName = "pdf-input";

        // Optional: name of the container where extracted text files will be stored.
        // If you want to store them in the same container, keep this equal to containerName.
        const string outputContainerName = "pdf-output";

        // Create Blob service and container clients.
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        BlobContainerClient inputContainer = serviceClient.GetBlobContainerClient(containerName);
        BlobContainerClient outputContainer = serviceClient.GetBlobContainerClient(outputContainerName);

        // Ensure the output container exists.
        await outputContainer.CreateIfNotExistsAsync();

        // Iterate over all blobs in the input container.
        await foreach (BlobItem blobItem in inputContainer.GetBlobsAsync())
        {
            // Process only PDF files.
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue;

            Console.WriteLine($"Processing: {blobItem.Name}");

            // Download the PDF into a memory stream.
            BlobClient pdfBlob = inputContainer.GetBlobClient(blobItem.Name);
            using (MemoryStream pdfStream = new MemoryStream())
            {
                await pdfBlob.DownloadToAsync(pdfStream);
                pdfStream.Position = 0; // Reset for reading.

                // Extract text using Aspose.Pdf.Facades.PdfExtractor.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF stream to the extractor.
                    extractor.BindPdf(pdfStream);

                    // Extract text from the whole document (Unicode encoding is default).
                    extractor.ExtractText();

                    // Store extracted text into another memory stream.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0; // Reset for uploading.

                        // Prepare the name for the output text file.
                        string txtBlobName = Path.ChangeExtension(blobItem.Name!, ".txt");
                        BlobClient txtBlob = outputContainer.GetBlobClient(txtBlobName);

                        // Upload the extracted text.
                        await txtBlob.UploadAsync(textStream, overwrite: true);
                        Console.WriteLine($"Uploaded extracted text as: {txtBlobName}");
                    }
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
