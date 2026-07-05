using System;
using System.IO;
using Aspose.Pdf;                     // Aspose.Pdf core API
using Azure.Storage.Blobs;            // Azure Blob Storage SDK (stubbed for compilation)

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs – only the members used in this sample.
// In a real project add the official Azure.Storage.Blobs NuGet package.
// ---------------------------------------------------------------------------
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

        // In the real SDK this creates the container if it does not exist.
        // Here it is a no‑op because the stub writes to the local file system.
        public void CreateIfNotExists()
        {
            // No operation – placeholder for real SDK behavior.
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(_containerName, blobName);
        }
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

        // Mimics BlobClient.Upload – writes the stream to a local folder so the
        // sample can be executed without the real Azure SDK.
        public void Upload(Stream content, bool overwrite = false)
        {
            // Resolve a local path like "output/<container>/<blob>"
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "output");
            string containerPath = Path.Combine(basePath, _containerName);
            Directory.CreateDirectory(containerPath);
            string filePath = Path.Combine(containerPath, _blobName);

            // If overwrite is false and the file exists, throw to emulate SDK behaviour.
            if (!overwrite && File.Exists(filePath))
                throw new IOException($"Blob '{_blobName}' already exists in container '{_containerName}'.");

            // Write the stream to the file.
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
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

        // Azure Blob Storage connection details (placeholder values)
        const string connectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
        const string containerName   = "xfdf-annotations";
        const string blobName        = "annotations.xfdf";

        // Ensure the source PDF exists – create a minimal document if it does not.
        if (!File.Exists(pdfPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page so ExportAnnotationsToXfdf has a document to work with.
                placeholder.Pages.Add();
                placeholder.Save(pdfPath);
            }
        }

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(pdfPath))
        {
            // Export all annotations to XFDF using a memory stream (lifecycle: export)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                doc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream position before upload

                // Ensure the target container exists
                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
                container.CreateIfNotExists();

                // Upload the XFDF stream to the specified blob (cloud storage)
                BlobClient blob = container.GetBlobClient(blobName);
                blob.Upload(xfdfStream, overwrite: true);
            }
        }

        Console.WriteLine("Annotations exported to XFDF and uploaded to cloud storage.");
    }
}
