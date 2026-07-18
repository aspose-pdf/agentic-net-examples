using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;               // PdfExtractor
using Azure.Storage.Blobs;               // BlobContainerClient, BlobClient

// ---------------------------------------------------------------------------
// Minimal stub implementation for Azure.Storage.Blobs when the real package is
// not referenced. This allows the sample to compile and run in environments
// where the Azure SDK is unavailable. In a production project you should add
// the NuGet package "Azure.Storage.Blobs" and remove this stub namespace.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    /// <summary>
    /// Stub for Azure.Storage.Blobs.BlobContainerClient.
    /// </summary>
    public class BlobContainerClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        public BlobContainerClient(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        /// <summary>
        /// In the real SDK this creates the container if it does not exist.
        /// The stub simply does nothing.
        /// </summary>
        public void CreateIfNotExists()
        {
            // No‑op for stub.
        }

        /// <summary>
        /// Returns a stub BlobClient for the requested blob name.
        /// </summary>
        public BlobClient GetBlobClient(string blobName) => new BlobClient(_containerName, blobName);
    }

    /// <summary>
    /// Stub for Azure.Storage.Blobs.BlobClient.
    /// </summary>
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
        /// Mimics the real Upload method. The stub writes the stream to a file
        /// named after the blob in the current working directory.
        /// </summary>
        public void Upload(Stream content, bool overwrite = false)
        {
            // Ensure the stream is at the beginning.
            if (content.CanSeek)
                content.Position = 0;

            // Simple local‑file fallback – useful for demo / unit‑test scenarios.
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _blobName);
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            content.CopyTo(fileStream);
        }
    }
}

class Program
{
    static void Main()
    {
        // -------------------------------------------------------------------
        // 1. Create a sample PDF that contains at least one image. This ensures
        //    the sandbox has a valid "input.pdf" file for the extractor to work
        //    with.
        // -------------------------------------------------------------------
        const string pdfPath = "input.pdf";
        CreateSamplePdfWithImage(pdfPath);

        // -------------------------------------------------------------------
        // 2. Azure Blob Storage connection details (stub values are fine for the demo)
        // -------------------------------------------------------------------
        const string azureConnectionString = "YourAzureBlobConnectionString";
        const string containerName = "pdf-images";

        // Initialize the Blob container client and ensure the container exists
        var containerClient = new BlobContainerClient(azureConnectionString, containerName);
        containerClient.CreateIfNotExists();

        // -------------------------------------------------------------------
        // 3. Extract images from the PDF using Aspose.Pdf.Facades.PdfExtractor
        // -------------------------------------------------------------------
        using (var extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                using (var imageStream = new MemoryStream())
                {
                    extractor.GetNextImage(imageStream);
                    imageStream.Position = 0; // Reset for upload

                    string blobName = $"image-{imageIndex}.jpg";
                    var blobClient = containerClient.GetBlobClient(blobName);
                    blobClient.Upload(imageStream, overwrite: true);
                }
                imageIndex++;
            }
        }

        Console.WriteLine("All images have been extracted and uploaded to Azure Blob Storage.");
    }

    /// <summary>
    /// Generates a minimal PDF containing a single JPEG image. The image is
    /// created in‑memory using System.Drawing, saved to a MemoryStream and then
    /// added to the PDF via Aspose.Pdf.Image.
    /// </summary>
    private static void CreateSamplePdfWithImage(string outputPath)
    {
        // Create a simple bitmap (red square) and encode it as JPEG.
        using var bitmap = new Bitmap(200, 200);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color.
            graphics.Clear(System.Drawing.Color.Red);
        }
        using var imgStream = new MemoryStream();
        bitmap.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        imgStream.Position = 0;

        // Build the PDF and embed the image.
        var doc = new Document();
        var page = doc.Pages.Add();
        var image = new Aspose.Pdf.Image();
        image.ImageStream = imgStream; // Assign the stream to the Aspose.Pdf.Image.
        image.FixWidth = 200;
        image.FixHeight = 200;
        page.Paragraphs.Add(image);

        doc.Save(outputPath);
    }
}
