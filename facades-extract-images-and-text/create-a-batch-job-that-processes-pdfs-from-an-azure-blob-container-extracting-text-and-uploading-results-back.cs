using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// referenced. They provide just enough members for the sample code to compile
// and run (no actual Azure interaction).
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobContainerClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly Dictionary<string, byte[]> _store = new(); // in‑memory fake store

        public BlobContainerClient(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        public async Task CreateIfNotExistsAsync()
        {
            // No‑op for the stub – in a real scenario the container would be created.
            await Task.CompletedTask;
        }

        public async IAsyncEnumerable<BlobItem> GetBlobsAsync()
        {
            // Return any blobs that have been "uploaded" via the stub BlobClient.
            foreach (var name in _store.Keys)
            {
                yield return new BlobItem { Name = name };
                await Task.Yield();
            }
        }

        public BlobClient GetBlobClient(string blobName) => new BlobClient(this, blobName);

        // Internal helper used by BlobClient to store/retrieve data.
        internal void Store(string name, byte[] data) => _store[name] = data;
        internal byte[] Retrieve(string name) => _store.TryGetValue(name, out var data) ? data : Array.Empty<byte>();
    }

    public class BlobClient
    {
        private readonly BlobContainerClient _container;
        private readonly string _blobName;

        public BlobClient(BlobContainerClient container, string blobName)
        {
            _container = container;
            _blobName = blobName;
        }

        public async Task DownloadToAsync(Stream destination)
        {
            var data = _container.Retrieve(_blobName);
            await destination.WriteAsync(data, 0, data.Length);
            await destination.FlushAsync();
        }

        public async Task UploadAsync(Stream source, bool overwrite = false)
        {
            using var ms = new MemoryStream();
            await source.CopyToAsync(ms);
            _container.Store(_blobName, ms.ToArray());
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; } = string.Empty;
    }
}

class Program
{
    // Entry point – async Main is supported in .NET Core 3.0+.
    static async Task Main(string[] args)
    {
        // Azure Blob Storage connection details.
        const string connectionString = "<YOUR_AZURE_BLOB_CONNECTION_STRING>";
        const string inputContainerName = "pdfs";               // Container with source PDFs.
        const string outputContainerName = "extracted-text";    // Container for text results.

        // Clients for input and output containers.
        BlobContainerClient inputContainer = new BlobContainerClient(connectionString, inputContainerName);
        BlobContainerClient outputContainer = new BlobContainerClient(connectionString, outputContainerName);

        // Ensure the output container exists.
        await outputContainer.CreateIfNotExistsAsync();

        // Enumerate all blobs in the input container.
        await foreach (BlobItem blobItem in inputContainer.GetBlobsAsync())
        {
            // Process only PDF files.
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue;

            BlobClient inputBlob = inputContainer.GetBlobClient(blobItem.Name);

            // Download the PDF into a memory stream.
            using (MemoryStream pdfStream = new MemoryStream())
            {
                await inputBlob.DownloadToAsync(pdfStream);
                pdfStream.Position = 0; // Reset for reading.

                // Use Aspose.Pdf.Facades.PdfExtractor to extract text.
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    // Bind the PDF stream to the extractor.
                    extractor.BindPdf(pdfStream);

                    // Extract all text using Unicode encoding (default).
                    extractor.ExtractText();

                    // Retrieve the extracted text into another memory stream.
                    using (MemoryStream textStream = new MemoryStream())
                    {
                        extractor.GetText(textStream);
                        textStream.Position = 0; // Reset for upload.

                        // Prepare the output blob name (same as PDF but .txt extension).
                        string txtBlobName = Path.ChangeExtension(blobItem.Name, ".txt");
                        BlobClient outputBlob = outputContainer.GetBlobClient(txtBlobName);

                        // Upload the extracted text.
                        await outputBlob.UploadAsync(textStream, overwrite: true);
                    }
                }
            }

            Console.WriteLine($"Processed and uploaded text for: {blobItem.Name}");
        }

        Console.WriteLine("Batch extraction completed.");
    }
}
