using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// referenced. They provide just enough members for the sample code to compile
// and run (no actual Azure interaction).
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
        public BlobContainerClient(string containerName) => _containerName = containerName;
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
        public async IAsyncEnumerable<BlobItem> GetBlobsAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            // In a real implementation this would enumerate blobs in the container.
            // For the stub we simply return an empty sequence.
            await Task.Yield();
            yield break;
        }
    }

    public class BlobClient
    {
        private readonly string _blobName;
        public BlobClient(string blobName) => _blobName = blobName;
        public Task DownloadToAsync(Stream destination) => Task.CompletedTask;
        public Task UploadAsync(Stream source, bool overwrite = false) => Task.CompletedTask;
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; }
    }
}

namespace PdfBatchExtraction
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Azure Blob storage connection settings (replace with real values).
            string connectionString = "YourAzureBlobConnectionString";
            string sourceContainerName = "pdf-container";
            string destinationContainerName = "text-output-container";

            // Create Blob service and container clients.
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient sourceContainer = blobServiceClient.GetBlobContainerClient(sourceContainerName);
            BlobContainerClient destinationContainer = blobServiceClient.GetBlobContainerClient(destinationContainerName);

            // Ensure the destination container exists.
            await destinationContainer.CreateIfNotExistsAsync();

            // Iterate over all PDF blobs in the source container.
            await foreach (BlobItem blobItem in sourceContainer.GetBlobsAsync())
            {
                if (blobItem.Name != null && blobItem.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    BlobClient sourceBlob = sourceContainer.GetBlobClient(blobItem.Name);

                    // Download PDF content into a memory stream.
                    using (MemoryStream pdfStream = new MemoryStream())
                    {
                        await sourceBlob.DownloadToAsync(pdfStream);
                        pdfStream.Position = 0;

                        // Load the PDF document using Aspose.Pdf.
                        using (Document pdfDocument = new Document(pdfStream))
                        {
                            // Extract all text from the document.
                            TextAbsorber textAbsorber = new TextAbsorber();
                            pdfDocument.Pages.Accept(textAbsorber);
                            string extractedText = textAbsorber.Text;

                            // Prepare the destination blob (same name with .txt extension).
                            string textBlobName = Path.ChangeExtension(blobItem.Name, ".txt");
                            BlobClient destinationBlob = destinationContainer.GetBlobClient(textBlobName);

                            // Upload the extracted text as UTF‑8.
                            byte[] textBytes = Encoding.UTF8.GetBytes(extractedText);
                            using (MemoryStream textStream = new MemoryStream(textBytes))
                            {
                                await destinationBlob.UploadAsync(textStream, overwrite: true);
                            }
                        }
                    }
                }
            }
        }
    }
}
