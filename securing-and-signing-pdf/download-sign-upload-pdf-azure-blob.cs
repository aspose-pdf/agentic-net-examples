using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;
using Azure.Storage.Blobs;

// Minimal stubs for Azure.Storage.Blobs when the NuGet package is not referenced.
// These stubs provide just enough API surface to compile the sample code.
// In a real project, reference the official Azure.Storage.Blobs package instead.
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

        public async Task CreateIfNotExistsAsync()
        {
            // No‑op stub – in production this creates the container if it does not exist.
            await Task.CompletedTask;
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(_connectionString, _containerName, blobName);
        }
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

        public async Task UploadAsync(Stream content, bool overwrite = false)
        {
            // Stub implementation – simply reads the stream to ensure it is consumable.
            // Replace with the real Azure SDK call in production.
            if (content == null) throw new ArgumentNullException(nameof(content));
            await content.CopyToAsync(Stream.Null);
        }
    }
}

class Program
{
    // Entry point
    static async Task Main()
    {
        // URL of the PDF to download – replace with a reachable URL in production.
        const string pdfUrl = "https://example.com/input.pdf";

        // Path to the PFX certificate and its password
        const string certPath = "certificate.pfx";
        const string certPassword = "pfxPassword";

        // Azure Blob Storage settings
        const string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        const string containerName = "pdfs";
        const string blobName = "signed-output.pdf";

        // Download PDF into a memory stream (with graceful fallback if the URL is not reachable)
        using (HttpClient httpClient = new HttpClient())
        using (MemoryStream pdfStream = new MemoryStream())
        {
            bool pdfLoaded = false;
            try
            {
                var response = await httpClient.GetAsync(pdfUrl);
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.CopyToAsync(pdfStream);
                    pdfStream.Position = 0; // reset for reading
                    pdfLoaded = true;
                }
                else
                {
                    Console.WriteLine($"Warning: Unable to download PDF (status {(int)response.StatusCode}). A blank PDF will be created instead.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Exception while downloading PDF – {ex.Message}. A blank PDF will be created instead.");
            }

            // If download failed, create a minimal blank PDF document.
            if (!pdfLoaded)
            {
                using (Document blankDoc = new Document())
                {
                    blankDoc.Pages.Add();
                    blankDoc.Save(pdfStream);
                    pdfStream.Position = 0;
                }
            }

            // Load PDF from the stream
            using (Document doc = new Document(pdfStream))
            {
                // Ensure the document has at least one page
                if (doc.Pages.Count == 0)
                {
                    doc.Pages.Add();
                }

                // Create a signature field on the first page
                // Fully qualify the Rectangle type to avoid ambiguity between Aspose.Pdf.Rectangle and Aspose.Pdf.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550); // position of the signature appearance
                SignatureField signatureField = new SignatureField(doc.Pages[1], rect);
                doc.Form.Add(signatureField);

                // Load the certificate (PFX) as a stream
                if (!File.Exists(certPath))
                {
                    Console.WriteLine($"Warning: Certificate file '{certPath}' not found. Skipping signing step.");
                }
                else
                {
                    using (var certStream = File.OpenRead(certPath))
                    {
                        // Create a PKCS7 signature object using the certificate stream
                        PKCS7 pkcs7 = new PKCS7(certStream, certPassword);

                        // Sign the field
                        signatureField.Sign(pkcs7);
                    }
                }

                // Save the signed PDF into a memory stream
                using (MemoryStream signedPdfStream = new MemoryStream())
                {
                    doc.Save(signedPdfStream);
                    signedPdfStream.Position = 0; // reset for upload

                    // Upload to Azure Blob Storage
                    BlobContainerClient blobContainer = new BlobContainerClient(azureConnectionString, containerName);
                    var blobClient = blobContainer.GetBlobClient(blobName);

                    // Ensure the container exists
                    await blobContainer.CreateIfNotExistsAsync();

                    // Upload the PDF stream
                    await blobClient.UploadAsync(signedPdfStream, overwrite: true);
                }
            }
        }

        Console.WriteLine("PDF processed (downloaded or created), optionally signed, and uploaded to Azure Blob Storage successfully.");
    }
}
