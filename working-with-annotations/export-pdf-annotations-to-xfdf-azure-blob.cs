using System;
using System.IO;
using Aspose.Pdf;
using Azure.Storage.Blobs;

// Minimal stubs for Azure.Storage.Blobs to allow compilation without the actual NuGet package.
// In a real project, reference the official Azure.Storage.Blobs package instead.
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
        public void CreateIfNotExists() { /* No‑op for stub */ }
        public BlobClient GetBlobClient(string blobName) => new BlobClient(_containerName, blobName);
    }

    public class BlobClient
    {
        private readonly string _containerName;
        private readonly string _blobName;
        public BlobClient(string containerName, string blobName)
        {
            _containerName = containerName;
            _blobName = blobName;
        }
        // Simple stub that writes the stream to a local file under a folder named after the container.
        public void Upload(Stream content, bool overwrite = false)
        {
            string directory = Path.Combine(Directory.GetCurrentDirectory(), _containerName);
            Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, _blobName);
            if (File.Exists(filePath) && !overwrite)
                throw new IOException($"Blob '{_blobName}' already exists in container '{_containerName}'.");
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                content.CopyTo(fileStream);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        // Path to the source PDF file containing annotations
        const string pdfPath = "input.pdf";

        // Azure Blob Storage configuration (replace with your actual values)
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        string containerName    = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER");

        if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(containerName))
        {
            Console.Error.WriteLine("Missing Azure Storage configuration.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Export annotations to an in‑memory XFDF stream
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream for reading

                // Prepare Azure Blob client (stub or real client if the package is referenced)
                BlobServiceClient blobService = new BlobServiceClient(connectionString);
                BlobContainerClient container = blobService.GetBlobContainerClient(containerName);
                container.CreateIfNotExists();

                // Determine the blob name (same as PDF but with .xfdf extension)
                string blobName = Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf";
                BlobClient blob = container.GetBlobClient(blobName);

                // Upload the XFDF stream to the blob storage
                blob.Upload(xfdfStream, overwrite: true);
                Console.WriteLine($"Annotations exported to XFDF and uploaded as '{blobName}'.");
            }
        }
    }
}
