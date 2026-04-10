using System;
using System.IO;
using System.Collections.Generic;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stubs for Azure.Storage.Blobs – used only to make the sample compile
// when the real Azure SDK is not referenced. In a real project you should add
// the NuGet package "Azure.Storage.Blobs" instead of these stubs.
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
        private readonly Dictionary<string, MemoryStream> _store = new Dictionary<string, MemoryStream>(StringComparer.OrdinalIgnoreCase);
        public BlobContainerClient(string containerName) => _containerName = containerName;
        public BlobClient GetBlobClient(string blobName) => new BlobClient(this, blobName);
        public void CreateIfNotExists() { /* No‑op for stub */ }
        public IEnumerable<BlobItem> GetBlobs()
        {
            foreach (var name in _store.Keys)
                yield return new BlobItem { Name = name };
        }
        internal MemoryStream GetOrCreateBlob(string name)
        {
            if (!_store.TryGetValue(name, out var ms))
            {
                ms = new MemoryStream();
                _store[name] = ms;
            }
            return ms;
        }
    }

    public class BlobClient
    {
        private readonly BlobContainerClient _container;
        private readonly string _blobName;
        public BlobClient(BlobContainerClient container, string blobName)
        {
            _container = container;
            _blobName = blobName;
        }
        public void DownloadTo(Stream destination)
        {
            var source = _container.GetOrCreateBlob(_blobName);
            source.Position = 0;
            source.CopyTo(destination);
        }
        public void Upload(Stream source, bool overwrite = false)
        {
            var target = _container.GetOrCreateBlob(_blobName);
            if (!overwrite) target.Position = target.Length; // append if not overwriting
            else { target.SetLength(0); target.Position = 0; }
            source.Position = 0;
            source.CopyTo(target);
        }
    }
}

namespace Azure.Storage.Blobs.Models
{
    public class BlobItem
    {
        public string Name { get; set; }
    }
}

class Program
{
    static void Main()
    {
        // Azure Blob Storage connection details
        const string connectionString = "<YOUR_AZURE_STORAGE_CONNECTION_STRING>";
        const string inputContainerName = "input-pdfs";
        const string outputContainerName = "output-pdfs";

        // Initialize Blob service and containers
        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        BlobContainerClient inputContainer = serviceClient.GetBlobContainerClient(inputContainerName);
        BlobContainerClient outputContainer = serviceClient.GetBlobContainerClient(outputContainerName);
        outputContainer.CreateIfNotExists();

        // Iterate over each PDF blob in the input container
        foreach (BlobItem blobItem in inputContainer.GetBlobs())
        {
            BlobClient inputBlob = inputContainer.GetBlobClient(blobItem.Name);

            // Download the PDF into a memory stream
            using (MemoryStream pdfInputStream = new MemoryStream())
            {
                inputBlob.DownloadTo(pdfInputStream);
                pdfInputStream.Position = 0; // Reset stream position for reading

                // Bind the stream to PdfAnnotationEditor
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdfInputStream);

                    // Example batch operation: flatten all annotations in the PDF
                    editor.FlatteningAnnotations();

                    // Save the modified PDF into another memory stream
                    using (MemoryStream pdfOutputStream = new MemoryStream())
                    {
                        editor.Save(pdfOutputStream);
                        pdfOutputStream.Position = 0; // Reset for upload

                        // Upload the processed PDF to the output container
                        BlobClient outputBlob = outputContainer.GetBlobClient(blobItem.Name);
                        outputBlob.Upload(pdfOutputStream, overwrite: true);
                    }
                }
            }

            Console.WriteLine($"Processed blob: {blobItem.Name}");
        }
    }
}
