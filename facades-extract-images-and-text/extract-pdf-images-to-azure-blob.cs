using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs (used when the real NuGet package is not
// referenced). These provide just enough functionality for compilation and a
// simple local‑file fallback for the Upload operation.
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
        public void CreateIfNotExists()
        {
            // No‑op for the stub – in a real scenario the container would be created.
        }
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
        /// <summary>
        /// Writes the stream to a local folder named "AzureBlobEmulation" mimicking
        /// an upload to Azure Blob Storage. The folder structure mirrors the container
        /// and blob names.
        /// </summary>
        public void Upload(Stream content, bool overwrite = false)
        {
            string basePath = Path.Combine(Directory.GetCurrentDirectory(), "AzureBlobEmulation");
            string containerPath = Path.Combine(basePath, _containerName);
            Directory.CreateDirectory(containerPath);
            string filePath = Path.Combine(containerPath, _blobName);
            if (!overwrite && File.Exists(filePath))
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
        string inputPdfPath = "sample.pdf";
        string azureConnectionString = "UseDevelopmentStorage=true"; // placeholder – not used by the stub
        string containerName = "pdf-images";

        // Ensure a PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(inputPdfPath);
        }

        // Initialize Azure Blob container client (stub works without the real SDK)
        var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(azureConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        containerClient.CreateIfNotExists();

        // Extract images from PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (MemoryStream imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0;

                    string blobName = $"image-{imageIndex}.png";
                    var blobClient = containerClient.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                }
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }
}
