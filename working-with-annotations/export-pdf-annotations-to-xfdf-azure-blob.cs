using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class AnnotationExporter
{
    static void Main()
    {
        // List of PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf" };
        // Target cloud storage bucket name
        string bucketName = "my-annotations-bucket";

        foreach (var pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"PDF not found: {pdfPath}");
                continue;
            }

            // Destination XFDF file path (same name, .xfdf extension)
            string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");

            // Load the PDF and export its annotations to XFDF
            using (Document pdfDoc = new Document(pdfPath))
            {
                // The overload that takes only the file name infers the format from the extension.
                // This works even when SaveFormat.Xfdf is not present in the referenced Aspose.Pdf version.
                pdfDoc.Save(xfdfPath);
            }

            // Upload the generated XFDF file to the cloud storage bucket
            try
            {
                UploadToBucket(xfdfPath, bucketName);
                Console.WriteLine($"Uploaded {xfdfPath} to bucket '{bucketName}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to upload {xfdfPath}: {ex.Message}");
            }

            // Optional: delete the local XFDF after successful upload
            // File.Delete(xfdfPath);
        }
    }

    // Placeholder method – replace with actual SDK calls for your cloud provider
    static void UploadToBucket(string filePath, string bucketName)
    {
        // Example pseudo‑code for a cloud SDK:
        // CloudStorageClient client = new CloudStorageClient(...);
        // using (var stream = File.OpenRead(filePath))
        // {
        //     client.UploadObject(bucketName, Path.GetFileName(filePath), stream);
        // }

        // For demonstration, just verify the file exists
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File to upload not found.", filePath);
    }
}
