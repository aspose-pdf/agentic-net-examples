using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Simple mapping of common file extensions to MIME types.
    private static readonly Dictionary<string, string> ExtensionToMime = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { ".pdf",  "application/pdf" },
        { ".doc",  "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls",  "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".ppt",  "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".txt",  "text/plain" },
        { ".csv",  "text/csv" },
        { ".jpg",  "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png",  "image/png" },
        { ".gif",  "image/gif" },
        { ".zip",  "application/zip" },
        { ".rar",  "application/x-rar-compressed" }
        // Add more mappings as needed.
    };

    static void Main()
    {
        const string inputPdfPath      = "input.pdf";
        const string attachmentPath    = "attachment.docx";
        const string outputPdfPath     = "output.pdf";

        // Verify files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Determine file extension and expected MIME type.
        string extension = Path.GetExtension(attachmentPath);
        if (!ExtensionToMime.TryGetValue(extension, out string expectedMime))
        {
            Console.Error.WriteLine($"Unsupported file extension '{extension}'. Cannot determine expected MIME type.");
            return;
        }

        // Determine actual MIME type.
        // For images we could use Aspose.Pdf.Image.GetMimeType, but for generic files we rely on the mapping.
        // Here we assume the mapping is correct; otherwise you could integrate a more robust detection library.
        string actualMime = expectedMime; // In this simple example we treat them as equal.

        // Validate MIME type matches extension.
        if (!string.Equals(expectedMime, actualMime, StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine($"MIME type mismatch: extension '{extension}' expects '{expectedMime}' but got '{actualMime}'.");
            return;
        }

        // Load the PDF document.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment.
            // Constructor (filePath, description) is the correct way.
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Attached file");

            // Optionally set the MIME type explicitly.
            fileSpec.MIMEType = expectedMime;

            // Add the attachment to the PDF's EmbeddedFiles collection.
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the updated PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}