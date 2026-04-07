using System;
using System.IO;
using Aspose.Pdf;
using Azure.Storage.Blobs;

// Minimal stubs for Azure.Storage.Blobs to allow compilation without the actual NuGet package.
// In a real project, reference the official Azure.Storage.Blobs package instead.
namespace Azure.Storage.Blobs
{
    public class BlobContainerClient
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _localPath;

        public BlobContainerClient(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
            // For the stub we map the container to a folder under the current directory.
            _localPath = Path.Combine(Directory.GetCurrentDirectory(), "BlobStorageStub", _containerName);
        }

        // Mimics the Azure SDK method – creates the folder if it does not exist.
        public void CreateIfNotExists()
        {
            Directory.CreateDirectory(_localPath);
        }

        public BlobClient GetBlobClient(string blobName)
        {
            return new BlobClient(Path.Combine(_localPath, blobName));
        }
    }

    public class BlobClient
    {
        private readonly string _blobPath;

        public BlobClient(string blobPath)
        {
            _blobPath = blobPath;
        }

        // Simple upload that writes the stream to a file on disk.
        public void Upload(Stream content, bool overwrite = false)
        {
            var mode = overwrite ? FileMode.Create : FileMode.CreateNew;
            using (var fileStream = new FileStream(_blobPath, mode, FileAccess.Write))
            {
                content.CopyTo(fileStream);
            }
        }
    }
}

class ExportAnnotationsToXfdfAndUpload
{
    static void Main()
    {
        // Path to the PDF file whose annotations will be exported
        const string pdfPath = "input.pdf";

        // Azure Blob Storage connection details (used only by the real SDK – kept for illustration)
        const string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=YOUR_ACCOUNT;AccountKey=YOUR_KEY;EndpointSuffix=core.windows.net";
        const string containerName = "xfdf-annotations";

        // Validate input file existence
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        try
        {
            // Ensure the target container exists (stub creates a local folder)
            BlobContainerClient containerClient = new BlobContainerClient(azureConnectionString, containerName);
            containerClient.CreateIfNotExists();

            // Load the PDF document
            using (Document pdfDocument = new Document(pdfPath))
            {
                // Export annotations to an in‑memory XFDF stream
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
                    xfdfStream.Position = 0; // Reset for reading

                    // Determine a blob name based on the source PDF file name
                    string pdfFileName = Path.GetFileNameWithoutExtension(pdfPath);
                    string xfdfBlobName = $"{pdfFileName}.xfdf";

                    // Upload the XFDF stream to Azure Blob Storage (or the stub implementation)
                    BlobClient blobClient = containerClient.GetBlobClient(xfdfBlobName);
                    blobClient.Upload(xfdfStream, overwrite: true);
                }
            }

            Console.WriteLine("Annotations exported to XFDF and uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
