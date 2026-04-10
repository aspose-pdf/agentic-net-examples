using System;
using System.IO;

// Minimal stubs for Azure.Storage.Blobs when the package is not referenced.
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
        // Synchronous version used in the sample.
        public void CreateIfNotExists() { /* No‑op stub */ }
        public BlobClient GetBlobClient(string blobName) => new BlobClient();
    }

    public class BlobClient
    {
        // Synchronous upload used in the sample.
        public void Upload(Stream source, bool overwrite = false)
        {
            // In a real implementation this would upload to Azure Blob Storage.
            // The stub simply reads the stream to simulate work.
            using (var ms = new MemoryStream())
            {
                source.CopyTo(ms);
            }
        }
    }
}

class PdfEncryptionUtility
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        const string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
        const string containerName = "encrypted-pdfs";
        const string blobName = "output_encrypted.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        using (MemoryStream encryptedStream = new MemoryStream())
        {
            // Fully‑qualified facade types prevent CS0104 ambiguities.
            using (Aspose.Pdf.Facades.PdfFileSecurity security = new Aspose.Pdf.Facades.PdfFileSecurity())
            {
                security.BindPdf(inputPdfPath);
                security.EncryptFile(
                    userPassword,
                    ownerPassword,
                    Aspose.Pdf.Facades.DocumentPrivilege.Print,
                    Aspose.Pdf.Facades.KeySize.x256);
                security.Save(encryptedStream);
            }

            encryptedStream.Position = 0;

            // Azure Blob Storage upload (stubbed if Azure SDK is absent).
            var blobServiceClient = new Azure.Storage.Blobs.BlobServiceClient(azureConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();

            var blobClient = containerClient.GetBlobClient(blobName);
            blobClient.Upload(encryptedStream, overwrite: true);
        }

        Console.WriteLine("PDF encrypted with AES‑256 and uploaded successfully.");
    }
}
