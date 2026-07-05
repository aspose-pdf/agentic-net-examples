using System;
using System.IO;
using System.Drawing.Imaging; // ImageFormat for extracted images
using System.Runtime.Versioning; // For OS platform attributes
using Aspose.Pdf.Facades; // PdfExtractor facade

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// referenced. These provide just enough members to compile the sample code.
// In a real project you should add the Azure.Storage.Blobs package instead.
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
        public void CreateIfNotExists() => Console.WriteLine($"[Stub] Created container '{_containerName}' if it did not exist.");
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
        public void Upload(Stream content, bool overwrite = false)
        {
            // In the stub we simply read the stream length and report it.
            long length = content.CanSeek ? content.Length : -1;
            Console.WriteLine($"[Stub] Uploaded blob '{_blobName}' to container '{_containerName}'. Size: {(length >= 0 ? length + " bytes" : "unknown size")}, Overwrite: {overwrite}");
        }
    }
}

// The Models namespace is referenced in the original code but not used directly.
namespace Azure.Storage.Blobs.Models { }

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Azure Blob Storage connection details (replace with real values when using the real SDK)
        const string connectionString = "<YOUR_AZURE_BLOB_CONNECTION_STRING>";
        const string containerName = "pdf-images";

        // Ensure the target container exists
        var blobService = new Azure.Storage.Blobs.BlobServiceClient(connectionString);
        var container = blobService.GetBlobContainerClient(containerName);
        container.CreateIfNotExists();

        // Verify that the PDF file actually exists before trying to process it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: Could not find file '{pdfPath}'. Ensure the file exists and the path is correct.");
            return;
        }

        // Use PdfExtractor within a using block for deterministic disposal
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Start the image extraction process
            extractor.ExtractImage();

            int imageIndex = 1;

            // Iterate over all extracted images
            while (extractor.HasNextImage())
            {
                // Store the current image in a memory stream (PNG format)
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // The GetNextImage overload that takes ImageFormat is Windows‑only.
                    // Suppress the CA1416 warning because the code is intentionally
                    // platform‑specific (Aspose.Pdf requires System.Drawing on Windows).
#pragma warning disable CA1416 // Validate platform compatibility
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
#pragma warning restore CA1416 // Validate platform compatibility

                    imageStream.Position = 0; // Reset stream position for upload

                    // Define a unique blob name for each image
                    string blobName = $"image-{imageIndex}.png";

                    // Upload the image stream to Azure Blob Storage
                    var blobClient = container.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                }

                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and uploaded to Azure Blob Storage.");
    }
}
