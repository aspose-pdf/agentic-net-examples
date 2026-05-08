using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string attachmentPath = "sample.png"; // file to attach

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

        // Determine expected MIME type from file extension
        string expectedMime = GetMimeTypeFromExtension(Path.GetExtension(attachmentPath));

        // Determine actual MIME type (for images use Aspose.Pdf.Image.GetMimeType)
        string actualMime = GetActualMimeType(attachmentPath);

        // Validate MIME type matches extension
        if (!string.Equals(expectedMime, actualMime, StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine($"MIME type mismatch: extension suggests '{expectedMime}' but detected '{actualMime}'.");
            return;
        }

        // Load PDF, embed file, and add attachment annotation
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a FileSpecification for the attachment using a stream (required by Aspose.Pdf core API)
            var fileSpec = new FileSpecification(attachmentPath, "Attachment");
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
            fileSpec.MIMEType = actualMime;

            // Add the file specification to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Create a rectangle for the annotation (float values are accepted by the constructor)
            var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Add the attachment annotation to the first page (1‑based indexing)
            Page page = doc.Pages[1];
            var attachment = new FileAttachmentAnnotation(page, rect, fileSpec);
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }

    // Maps common file extensions to MIME types
    static string GetMimeTypeFromExtension(string extension)
    {
        if (string.IsNullOrEmpty(extension))
            return "application/octet-stream";

        switch (extension.ToLowerInvariant())
        {
            case ".png":  return "image/png";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            case ".gif":  return "image/gif";
            case ".bmp":  return "image/bmp";
            case ".pdf":  return "application/pdf";
            case ".txt":  return "text/plain";
            case ".doc":  return "application/msword";
            case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            case ".xls":  return "application/vnd.ms-excel";
            case ".xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            default:        return "application/octet-stream";
        }
    }

    // Retrieves the actual MIME type of a file.
    // For image files we use Aspose.Pdf.Image.GetMimeType; otherwise we fall back to the extension‑based MIME.
    static string GetActualMimeType(string filePath)
    {
        string ext = Path.GetExtension(filePath).ToLowerInvariant();
        var imageExtensions = new HashSet<string> { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

        if (imageExtensions.Contains(ext))
        {
            // Load the image using System.Drawing (Aspose.Pdf.Image.GetMimeType expects System.Drawing.Image)
            using (System.Drawing.Image img = System.Drawing.Image.FromFile(filePath))
            {
                return Aspose.Pdf.Image.GetMimeType(img);
            }
        }

        // Non‑image files: rely on the extension‑derived MIME type
        return GetMimeTypeFromExtension(ext);
    }
}
