using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class AttachmentValidator
{
    // Simple mapping of common file extensions to expected MIME types
    private static readonly Dictionary<string, string> ExtensionToMime = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { ".png",  "image/png" },
        { ".jpg",  "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif",  "image/gif" },
        { ".bmp",  "image/bmp" },
        { ".tif",  "image/tiff" },
        { ".tiff", "image/tiff" },
        { ".pdf",  "application/pdf" },
        { ".txt",  "text/plain" },
        { ".doc",  "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls",  "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
        // Add more mappings as needed
    };

    // Determines MIME type for image files using Aspose.Pdf.Image.GetMimeType
    private static string GetImageMimeType(string filePath)
    {
        // Load the image using System.Drawing.Image and let Aspose.Pdf.Image detect the MIME type
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(filePath))
        {
            return Aspose.Pdf.Image.GetMimeType(img);
        }
    }

    // Determines MIME type based on file extension (fallback for non‑image files)
    private static string GetMimeTypeByExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath);
        if (ExtensionToMime.TryGetValue(ext, out string mime))
            return mime;
        return "application/octet-stream"; // unknown
    }

    // Validates that the detected MIME type matches the expected MIME type for the extension
    private static void ValidateMime(string filePath)
    {
        string ext = Path.GetExtension(filePath);
        string expectedMime = GetMimeTypeByExtension(filePath);
        string actualMime;

        // Use image-specific detection for known image extensions
        if (ext.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".gif", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".bmp", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".tif", StringComparison.OrdinalIgnoreCase) ||
            ext.Equals(".tiff", StringComparison.OrdinalIgnoreCase))
        {
            actualMime = GetImageMimeType(filePath);
        }
        else
        {
            actualMime = GetMimeTypeByExtension(filePath);
        }

        if (!string.Equals(expectedMime, actualMime, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"MIME type mismatch for '{Path.GetFileName(filePath)}'. Expected: {expectedMime}, Detected: {actualMime}");
        }
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string attachmentPath = "sample.png";       // file to attach
        const string outputPdfPath = "output.pdf";        // result PDF

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Validate MIME type before proceeding
            ValidateMime(attachmentPath);

            // Load the PDF (using the lifecycle rule for disposal)
            using (Document doc = new Document(inputPdfPath))
            {
                // Create a FileSpecification for the attachment (description is optional)
                FileSpecification fileSpec = new FileSpecification(attachmentPath, "Attached file");
                fileSpec.Description = "Attachment added by AttachmentValidator";

                // Add the file to the document's embedded files collection
                doc.EmbeddedFiles.Add(fileSpec);

                // Choose a page to place the attachment annotation (first page)
                Page page = doc.Pages[1];

                // Define the rectangle for the annotation (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);

                // Create the file attachment annotation
                FileAttachmentAnnotation attachmentAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    Icon = FileIcon.Paperclip, // visual icon – use FileIcon enum
                    Color = Aspose.Pdf.Color.Blue,
                    Contents = $"Attached file: {Path.GetFileName(attachmentPath)}"
                };

                // Add the annotation to the page
                page.Annotations.Add(attachmentAnnot);

                // Save the modified PDF (using the provided save rule)
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
