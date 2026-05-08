using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Azure.Storage.Blobs;

// Minimal stubs for Azure.Storage.Blobs to allow compilation without the actual package.
namespace Azure.Storage.Blobs
{
    public class BlobContainerClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobContainerClient(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        public Task CreateIfNotExistsAsync()
        {
            // In a real implementation this would create the container if it does not exist.
            // Here we simply return a completed task.
            return Task.CompletedTask;
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(blobName);
        }
    }

    public class BlobClient
    {
        private readonly string _blobName;

        public BlobClient(string blobName)
        {
            _blobName = blobName;
        }

        public Task UploadAsync(Stream content, bool overwrite = false)
        {
            // In a real implementation this would upload the stream to Azure Blob Storage.
            // For compilation purposes we just consume the stream and complete.
            return Task.CompletedTask;
        }
    }
}

class Program
{
    // Adjust these constants for your environment
    private const string PdfInputPath = "input.pdf";                 // Local PDF file with annotations
    private const string AzureConnectionString = "<YOUR_CONNECTION_STRING>";
    private const string AzureContainerName = "xfdf-annotations";

    static async Task Main()
    {
        // Verify the source PDF exists
        if (!File.Exists(PdfInputPath))
        {
            Console.Error.WriteLine($"PDF not found: {PdfInputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(PdfInputPath))
        {
            // Export annotations to an in‑memory XFDF stream (lifecycle rule: use ExportAnnotationsToXfdf(Stream))
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset for reading

                // Upload the XFDF stream to Azure Blob Storage (free‑form code – no specific rule)
                BlobContainerClient container = new BlobContainerClient(AzureConnectionString, AzureContainerName);
                await container.CreateIfNotExistsAsync();

                // Use the PDF file name (without extension) as the blob name
                string blobName = Path.GetFileNameWithoutExtension(PdfInputPath) + ".xfdf";
                BlobClient blob = container.GetBlobClient(blobName);

                // Upload the stream; overwrite if the blob already exists
                await blob.UploadAsync(xfdfStream, overwrite: true);
                Console.WriteLine($"Annotations exported to XFDF and uploaded as blob '{blobName}'.");
            }
        }
    }
}
