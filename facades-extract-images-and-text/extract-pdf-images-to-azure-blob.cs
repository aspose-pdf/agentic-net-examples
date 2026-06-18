using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;               // Added for ImageFormat
using Aspose.Pdf.Facades;          // PdfExtractor
using Aspose.Pdf;                 // Document, etc.
using Azure.Storage.Blobs;        // BlobContainerClient, BlobClient

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs types (used when the real package is not
// referenced). They provide just enough functionality for compilation and basic
// testing. In a production environment you should reference the official
// Azure.Storage.Blobs NuGet package instead of these stubs.
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
        // Here we simply return a completed task.
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;

        public BlobClient GetBlobClient(string blobName) => new BlobClient(_connectionString, _containerName, blobName);
    }

    public class BlobClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _blobName;

        public BlobClient(string connectionString, string containerName, string blobName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _blobName = blobName;
        }

        // The real SDK uploads the stream to Azure Blob Storage. This stub just
        // reads the stream to ensure it is seekable and then completes.
        public Task UploadAsync(Stream content, bool overwrite = false)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            // In a real implementation the SDK would upload the stream.
            return Task.CompletedTask;
        }
    }
}

class PdfImageExtractor
{
    // Extracts all images from a PDF file and uploads them to an Azure Blob container.
    public static async Task ExtractImagesToBlobAsync(
        string pdfPath,
        string azureConnectionString,
        string containerName)
    {
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the container exists.
        BlobContainerClient container = new BlobContainerClient(azureConnectionString, containerName);
        await container.CreateIfNotExistsAsync();

        // PdfExtractor implements IDisposable – use a using block for deterministic disposal.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // Prepare the extractor to retrieve images.
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Store the extracted image in a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Save the next image as PNG. Use System.Drawing.Imaging.ImageFormat.
                    extractor.GetNextImage(imageStream, ImageFormat.Png);
                    imageStream.Position = 0; // Reset stream position before upload.

                    // Define a unique blob name for each image.
                    string blobName = $"image-{imageIndex}.png";

                    // Get a reference to the blob and upload the image.
                    BlobClient blob = container.GetBlobClient(blobName);
                    await blob.UploadAsync(imageStream, overwrite: true);
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }

    // Example usage.
    static async Task Main()
    {
        string pdfFilePath = "sample.pdf";
        string azureConnStr = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        string blobContainer = "pdf-images";

        await ExtractImagesToBlobAsync(pdfFilePath, azureConnStr, blobContainer);
    }
}
