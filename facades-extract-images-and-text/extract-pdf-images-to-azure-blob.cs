using System;
using System.IO;
using Azure.Storage.Blobs;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // ImageFormat enum is defined here, but we will use the overload that does not require it

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// referenced. They provide just enough members for the sample code to compile
// and run in a local test environment. In a production project you should add
// the official Azure.Storage.Blobs package instead of these stubs.
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
        // The stub simply does nothing.
        public void CreateIfNotExists()
        {
            // No‑op for stub.
        }

        // Returns a client that can upload a blob with the given name.
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
    }

    public class BlobClient
    {
        private readonly string _blobName;

        public BlobClient(string blobName)
        {
            _blobName = blobName;
        }

        // Stub implementation writes the stream to a file in the current
        // working directory. The real SDK streams the data to Azure Blob
        // Storage.
        public void Upload(Stream content, bool overwrite = false)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _blobName);
            // Ensure the directory exists (it will, because we use the cwd).
            using var fileStream = File.Create(filePath);
            content.CopyTo(fileStream);
        }
    }
}

class PdfImageExtractor
{
    // Extracts all images from a PDF and uploads them to an Azure Blob container.
    public static void ExtractImagesToBlob(string pdfPath, string azureConnectionString, string containerName)
    {
        // Validate input file.
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Initialize Blob container client.
        BlobContainerClient containerClient = new BlobContainerClient(azureConnectionString, containerName);
        containerClient.CreateIfNotExists();

        // Use Aspose.Pdf.Facades.PdfExtractor to extract images.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF file to the extractor.
            extractor.BindPdf(pdfPath);

            // Perform the image extraction operation.
            extractor.ExtractImage();

            int imageIndex = 1;
            // Iterate over all extracted images.
            while (extractor.HasNextImage())
            {
                // Store the current image in a memory stream.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    // Save the image using the overload that does not require ImageFormat.
                    // The image will be saved in its original format.
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset stream position before upload.

                    // Define a unique blob name for each image. We keep the original
                    // extension if possible; otherwise default to .png.
                    string blobName = $"image-{imageIndex}.png";

                    // Get a reference to the blob and upload the image.
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                }

                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and upload completed.");
    }

    // Example usage.
    static void Main()
    {
        string pdfFilePath = "sample.pdf";
        string azureConnStr = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        string container = "pdf-images";

        ExtractImagesToBlob(pdfFilePath, azureConnStr, container);
    }
}
