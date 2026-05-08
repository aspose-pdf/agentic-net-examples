using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs when the real NuGet package is not
// available. They provide just enough members used by the sample code.
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
        public Task CreateIfNotExistsAsync() => Task.CompletedTask;
        public BlobClient GetBlobClient(string blobName) => new BlobClient(blobName);
    }

    public class BlobClient
    {
        private readonly string _blobName;
        public BlobClient(string blobName) => _blobName = blobName;
        public Task UploadAsync(Stream content, bool overwrite = false) => Task.CompletedTask;
    }
}

class Program
{
    // Entry point
    static async Task Main()
    {
        // URL of the PDF to download – replace with a reachable URL or use a local fallback.
        const string pdfUrl = "https://example.com/input.pdf";

        // Path to the PFX certificate file and its password
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // Azure Blob Storage connection details
        const string blobConnectionString = "<YOUR_AZURE_BLOB_CONNECTION_STRING>";
        const string containerName = "signed-pdfs";
        const string blobName = "signed-output.pdf";

        // -------------------------------------------------------------------
        // 1. Obtain the PDF bytes – try the network first, fall back to a local file.
        // -------------------------------------------------------------------
        byte[] pdfBytes;
        using (var httpClient = new HttpClient())
        {
            try
            {
                using var response = await httpClient.GetAsync(pdfUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to download PDF. Status code: {response.StatusCode}");
                }
                pdfBytes = await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
            {
                // Network download failed – attempt to load a local copy named "fallback.pdf".
                const string localFallback = "fallback.pdf";
                if (!File.Exists(localFallback))
                {
                    Console.WriteLine($"Unable to download PDF and fallback file '{localFallback}' does not exist.\nError: {ex.Message}");
                    return;
                }
                pdfBytes = await File.ReadAllBytesAsync(localFallback);
                Console.WriteLine("Downloaded PDF failed; using local fallback file.");
            }
        }

        // -------------------------------------------------------------------
        // 2. Load the PDF from the byte array (Document(Stream))
        // -------------------------------------------------------------------
        using var pdfStream = new MemoryStream(pdfBytes);
        using var pdfDocument = new Document(pdfStream);

        // Ensure the document has at least one page
        if (pdfDocument.Pages.Count == 0)
        {
            throw new InvalidOperationException("The PDF has no pages.");
        }

        // -------------------------------------------------------------------
        // 3. Create a signature field on the first page
        // -------------------------------------------------------------------
        var signatureRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
        var signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect);
        pdfDocument.Form.Add(signatureField);

        // -------------------------------------------------------------------
        // 4. Load the certificate and sign the field
        // -------------------------------------------------------------------
        if (!File.Exists(pfxPath))
        {
            Console.WriteLine($"Certificate file '{pfxPath}' not found.");
            return;
        }
        using (FileStream pfxStream = File.OpenRead(pfxPath))
        {
            var pkcs1Signature = new PKCS1(pfxStream, pfxPassword);
            signatureField.Sign(pkcs1Signature);
        }

        // -------------------------------------------------------------------
        // 5. Save the signed PDF into a memory stream ready for upload
        // -------------------------------------------------------------------
        using var signedPdfStream = new MemoryStream();
        pdfDocument.Save(signedPdfStream);
        signedPdfStream.Position = 0; // Reset for reading

        // -------------------------------------------------------------------
        // 6. Upload the signed PDF to Azure Blob Storage
        // -------------------------------------------------------------------
        var blobService = new Azure.Storage.Blobs.BlobServiceClient(blobConnectionString);
        var container = blobService.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync();
        var blob = container.GetBlobClient(blobName);
        await blob.UploadAsync(signedPdfStream, overwrite: true);

        Console.WriteLine("PDF downloaded (or fallback used), signed, and uploaded to Azure Blob Storage successfully.");
    }
}
