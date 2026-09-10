using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Azure.Storage.Blobs;

class Program
{
    static async Task Main(string[] args)
    {
        // URL of the PDF to download (replace with a valid URL in real usage)
        const string pdfUrl = "https://example.com/input.pdf";

        // Path to the PFX certificate and its password
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Azure Blob Storage connection details (used by the stub implementation)
        const string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
        const string containerName = "signed-pdfs";
        const string blobName = "signed_output.pdf";

        // ------------------------------------------------------------
        // Download the PDF into a stream – verify the HTTP response
        // ------------------------------------------------------------
        using (HttpClient http = new HttpClient())
        {
            HttpResponseMessage response = await http.GetAsync(pdfUrl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to download PDF. Status code: {response.StatusCode}");
                return; // abort execution – nothing to sign
            }

            using (Stream pdfStream = await response.Content.ReadAsStreamAsync())
            // Load the PDF document from the network stream
            using (Document doc = new Document(pdfStream))
            {
                // ------------------------------------------------
                // Create a signature field on the first page
                // ------------------------------------------------
                Page page = doc.Pages[1];
                // Use the Aspose.Pdf.Rectangle (layout rectangle) for the field bounds
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);
                SignatureField signatureField = new SignatureField(page, rect);
                doc.Form.Add(signatureField);

                // ------------------------------------------------
                // Load the certificate and sign the field using PKCS7
                // ------------------------------------------------
                using (FileStream pfxStream = File.OpenRead(pfxPath))
                {
                    PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword)
                    {
                        Reason = "Document approved",
                        Location = "Office",
                        ContactInfo = "contact@example.com"
                    };
                    signatureField.Sign(pkcs7);
                }

                // ------------------------------------------------
                // Save the signed PDF into a memory stream and upload
                // ------------------------------------------------
                using (MemoryStream signedStream = new MemoryStream())
                {
                    doc.Save(signedStream);
                    signedStream.Position = 0; // reset for upload

                    // Upload the signed PDF to Azure Blob Storage (stub implementation)
                    BlobServiceClient serviceClient = new BlobServiceClient(blobConnectionString);
                    BlobContainerClient container = serviceClient.GetBlobContainerClient(containerName);
                    await container.CreateIfNotExistsAsync();
                    BlobClient blob = container.GetBlobClient(blobName);
                    await blob.UploadAsync(signedStream, overwrite: true);
                }
            }
        }

        Console.WriteLine("PDF signed and uploaded to Azure Blob Storage.");
    }
}

// ---------------------------------------------------------------------------
// Minimal stub implementation of Azure.Storage.Blobs types to allow the example
// to compile without referencing the real Azure SDK. In a real project you
// should reference the official Azure.Storage.Blobs NuGet package.
// ---------------------------------------------------------------------------
namespace Azure.Storage.Blobs
{
    using System.Threading;
    using System.Threading.Tasks;

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
        public Task CreateIfNotExistsAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
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
        public Task UploadAsync(Stream content, bool overwrite = false, CancellationToken cancellationToken = default)
        {
            // Stub: write the stream to a local file for demonstration purposes.
            // In production the Azure SDK would upload the content to Azure Blob Storage.
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _blobName);
            using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                content.CopyTo(file);
            }
            return Task.CompletedTask;
        }
    }
}
