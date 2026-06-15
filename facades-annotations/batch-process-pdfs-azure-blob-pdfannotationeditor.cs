using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal in‑memory stubs for Azure Blob Storage SDK (used only to make the
// project compile when the real Azure SDK is not referenced).  They mimic the
// subset of the API required by the sample code: creating containers, listing
// blobs, reading a blob as a stream and uploading a stream.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobServiceClient
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, BlobContainerClient> _containers = new();

        public BlobServiceClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            if (!_containers.TryGetValue(containerName, out var client))
            {
                client = new BlobContainerClient(containerName);
                _containers[containerName] = client;
            }
            return client;
        }
    }

    public class BlobContainerClient
    {
        private readonly string _containerName;
        private readonly Dictionary<string, MemoryStream> _blobs = new();

        public BlobContainerClient(string containerName)
        {
            _containerName = containerName;
        }

        // In the real SDK this creates the container in Azure. Here we just
        // ensure the in‑memory dictionary exists.
        public Task CreateIfNotExistsAsync(CancellationToken cancellationToken = default)
        {
            // No‑op for the stub.
            return Task.CompletedTask;
        }

        public async IAsyncEnumerable<BlobItem> GetBlobsAsync([System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            foreach (var kvp in _blobs)
            {
                // Simulate async enumeration.
                await Task.Yield();
                yield return new BlobItem { Name = kvp.Key };
            }
        }

        public BlobClient GetBlobClient(string blobName) => new BlobClient(this, blobName);

        // Internal helper used by BlobClient to access the storage.
        internal MemoryStream GetOrCreateBlob(string name)
        {
            if (!_blobs.TryGetValue(name, out var ms))
            {
                ms = new MemoryStream();
                _blobs[name] = ms;
            }
            return ms;
        }
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

        // Returns a read‑only stream positioned at the beginning of the blob.
        public Task<Stream> OpenReadAsync(CancellationToken cancellationToken = default)
        {
            var source = _container.GetOrCreateBlob(_blobName);
            // Return a copy so the caller can read without affecting the stored stream.
            var copy = new MemoryStream(source.ToArray(), writable: false);
            return Task.FromResult<Stream>(copy);
        }

        // Upload replaces the existing content.
        public Task UploadAsync(Stream content, bool overwrite = false, CancellationToken cancellationToken = default)
        {
            var target = _container.GetOrCreateBlob(_blobName);
            target.SetLength(0);
            content.CopyTo(target);
            target.Position = 0;
            return Task.CompletedTask;
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; }
    }
}

class Program
{
    // Adjust these constants as needed
    private const string BlobConnectionString = "YourAzureBlobStorageConnectionString";
    private const string SourceContainerName = "source-pdf-container";
    private const string DestinationContainerName = "processed-pdf-container";

    static async Task Main()
    {
        // Initialize Blob service clients
        BlobServiceClient serviceClient = new BlobServiceClient(BlobConnectionString);
        BlobContainerClient sourceContainer = serviceClient.GetBlobContainerClient(SourceContainerName);
        BlobContainerClient destContainer = serviceClient.GetBlobContainerClient(DestinationContainerName);

        // Ensure destination container exists
        await destContainer.CreateIfNotExistsAsync();

        // Iterate over all blobs in the source container
        await foreach (BlobItem blobItem in sourceContainer.GetBlobsAsync())
        {
            // Process only PDF files
            if (!blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                continue;

            BlobClient sourceBlob = sourceContainer.GetBlobClient(blobItem.Name);
            BlobClient destBlob = destContainer.GetBlobClient(blobItem.Name);

            try
            {
                // Download the PDF as a stream
                using (Stream inputStream = await sourceBlob.OpenReadAsync())
                {
                    // Prepare a memory stream for the edited PDF
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        // Initialize PdfAnnotationEditor and bind the input stream
                        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                        {
                            editor.BindPdf(inputStream);

                            // Example operation: delete all annotations (replace with your logic)
                            editor.DeleteAnnotations();

                            // Save the edited PDF to the output stream
                            editor.Save(outputStream);
                        }

                        // Reset stream position before upload
                        outputStream.Position = 0;

                        // Upload the processed PDF to the destination container
                        await destBlob.UploadAsync(outputStream, overwrite: true);
                    }
                }

                Console.WriteLine($"Processed and uploaded: {blobItem.Name}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing blob '{blobItem.Name}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
