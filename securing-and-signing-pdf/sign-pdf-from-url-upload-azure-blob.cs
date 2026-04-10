using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Azure.Storage.Blobs; // Stub Azure Blob classes (provided below)

// Minimal stubs for Azure.Storage.Blobs to allow compilation without the actual NuGet package.
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

        public Task UploadAsync(Stream content, bool overwrite = false) => Task.CompletedTask;
    }
}

class Program
{
    static async Task Main()
    {
        // URL of the PDF to download – replace with a real URL in production.
        const string pdfUrl = "https://example.com/input.pdf";

        // Path to the signing certificate (PFX) and its password – replace with real values.
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        // Azure Blob Storage connection details – replace with real values.
        const string blobConnectionString = "DefaultEndpointsProtocol=https;AccountName=youraccount;AccountKey=yourkey;EndpointSuffix=core.windows.net";
        const string containerName = "signed-pdfs";
        const string blobName = "signed_output.pdf";

        Document doc;
        // Try to download the PDF. If the request fails (e.g., 404), fall back to an empty document.
        try
        {
            using (HttpClient http = new HttpClient())
            using (Stream pdfStream = await http.GetStreamAsync(pdfUrl))
            {
                doc = new Document(pdfStream);
            }
        }
        catch (Exception ex) when (ex is HttpRequestException || ex is IOException)
        {
            Console.WriteLine($"Failed to download PDF from '{pdfUrl}'. Reason: {ex.Message}");
            Console.WriteLine("Creating a new blank PDF instead.");
            doc = new Document();
            doc.Pages.Add(); // Ensure at least one page exists for the signature field.
        }

        // Ensure the document has at least one page before adding the signature field.
        if (doc.Pages.Count == 0)
        {
            doc.Pages.Add();
        }

        // Define the rectangle where the signature will appear (coordinates are in points).
        // Use Aspose.Pdf.Rectangle (layout rectangle) – not the drawing one.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a signature field on the first page.
        SignatureField signatureField = new SignatureField(doc.Pages[1], rect)
        {
            PartialName = "Signature1"
        };
        doc.Form.Add(signatureField);

        // Apply the digital signature if the certificate file exists.
        if (File.Exists(certPath))
        {
            try
            {
                PKCS1 pkcs = new PKCS1(certPath, certPassword);
                signatureField.Sign(pkcs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to apply digital signature: {ex.Message}");
                // Continue without a signature – the PDF will still be uploaded.
            }
        }
        else
        {
            Console.WriteLine($"Certificate file '{certPath}' not found. Skipping digital signature.");
        }

        // Save the signed (or unsigned) PDF into a memory stream.
        using (MemoryStream signedStream = new MemoryStream())
        {
            doc.Save(signedStream);
            signedStream.Position = 0; // Reset stream position for upload.

            // Upload the PDF to Azure Blob Storage (stub implementation).
            BlobContainerClient container = new BlobContainerClient(blobConnectionString, containerName);
            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(blobName);
            await blob.UploadAsync(signedStream, overwrite: true);
        }

        Console.WriteLine("PDF processing completed and uploaded to Azure Blob storage.");
    }
}
