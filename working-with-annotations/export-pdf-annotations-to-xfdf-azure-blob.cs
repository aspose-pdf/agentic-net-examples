using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Azure.Storage.Blobs;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure a PDF exists – create a minimal one with an annotation if missing
        if (!File.Exists(pdfPath))
        {
            using var seed = new Document();
            var page = seed.Pages.Add();
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            var annotation = new TextAnnotation(page, rect)
            {
                Title = "Sample",
                Contents = "Demo annotation for export"
            };
            page.Annotations.Add(annotation);
            seed.Save(pdfPath);
        }

        // Azure Blob Storage connection string and container name (placeholder values)
        const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
        const string containerName = "xfdf-annotations";

        ExportAnnotationsToCloud(pdfPath, storageConnectionString, containerName);
    }

    static void ExportAnnotationsToCloud(string pdfFilePath, string storageConnectionString, string containerName)
    {
        // Load the PDF document (lifecycle: create/load)
        using (Document pdfDoc = new Document(pdfFilePath))
        {
            // Export all annotations to a memory stream in XFDF format (lifecycle: save via ExportAnnotationsToXfdf)
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0; // Reset stream position before upload

                // Initialize Azure Blob container client (stub implementation works locally)
                BlobContainerClient container = new BlobContainerClient(storageConnectionString, containerName);
                container.CreateIfNotExists();

                // Determine blob name (same as PDF but with .xfdf extension)
                string blobName = Path.GetFileNameWithoutExtension(pdfFilePath) + ".xfdf";

                // Upload the XFDF stream to the cloud storage bucket
                BlobClient blob = container.GetBlobClient(blobName);
                blob.Upload(xfdfStream, overwrite: true);
            }
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stub implementation for Azure.Storage.Blobs used only for compilation.
// In a real project you would reference the official Azure.Storage.Blobs NuGet package.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    public class BlobContainerClient
    {
        private readonly string _containerPath;
        public BlobContainerClient(string connectionString, string containerName)
        {
            // The connection string is ignored in the stub; we map the container to a local folder.
            // This folder will be created under the current working directory.
            _containerPath = Path.Combine(Directory.GetCurrentDirectory()!, "blobstorage", containerName);
        }

        public void CreateIfNotExists()
        {
            Directory.CreateDirectory(_containerPath!);
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(_containerPath, blobName);
        }
    }

    public class BlobClient
    {
        private readonly string _blobPath;
        public BlobClient(string containerPath, string blobName)
        {
            _blobPath = Path.Combine(containerPath, blobName);
        }

        public void Upload(Stream content, bool overwrite = false)
        {
            // Ensure the directory exists.
            string dir = Path.GetDirectoryName(_blobPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // If overwrite is false and the file exists, throw to mimic Azure behaviour.
            if (!overwrite && File.Exists(_blobPath))
                throw new InvalidOperationException($"Blob '{_blobPath}' already exists and overwrite is set to false.");

            // Write the stream to the file.
            using (FileStream fileStream = new FileStream(_blobPath, FileMode.Create, FileAccess.Write))
            {
                content.CopyTo(fileStream);
            }
        }
    }
}
