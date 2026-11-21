using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple MIME type lookup based on file extension
    static readonly Dictionary<string, string> MimeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { ".pdf",  "application/pdf" },
        { ".doc",  "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls",  "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".png",  "image/png" },
        { ".jpg",  "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif",  "image/gif" },
        { ".txt",  "text/plain" },
        // add more mappings as needed
    };

    // Returns the expected MIME type for a given file extension, or an empty string if unknown
    static string GetMimeType(string extension)
    {
        if (string.IsNullOrEmpty(extension))
            return string.Empty;

        if (!extension.StartsWith("."))
            extension = "." + extension;

        MimeMap.TryGetValue(extension, out string mime);
        // Ensure we never return null to avoid CS8603 warning
        return mime ?? string.Empty;
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string attachmentPath = "sample.docx"; // file to attach
        const string providedMime   = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

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

        // Validate MIME type against file extension
        string ext   = Path.GetExtension(attachmentPath);
        string mime  = GetMimeType(ext);

        if (string.IsNullOrEmpty(mime))
        {
            Console.Error.WriteLine($"Unsupported file extension: {ext}");
            return;
        }

        if (!string.Equals(mime, providedMime, StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine($"MIME type mismatch. Expected: {mime}, Provided: {providedMime}");
            return;
        }

        // All checks passed – proceed to embed the attachment
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the first page for the annotation (adjust as needed)
            Page page = doc.Pages[1];

            // Define a rectangle where the attachment icon will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);

            // Create a file specification for the attachment (description provided to avoid null)
            FileSpecification fileSpec = new FileSpecification(attachmentPath, "Attached file");
            fileSpec.Description = $"Attached file: {Path.GetFileName(attachmentPath)}";

            // Create the file attachment annotation
            FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set a visible name for the annotation
                Name = Path.GetFileName(attachmentPath)
            };

            // Optionally set the icon (uses the FileIcon enum, not a non‑existent Icons enum)
            attachment.Icon = FileIcon.PushPin;

            // Add the annotation to the page
            page.Annotations.Add(attachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}
