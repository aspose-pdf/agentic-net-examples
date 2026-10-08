using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Simple mapping of common file extensions to their MIME types.
    // Extend this dictionary as needed.
    private static readonly Dictionary<string, string> ExtensionToMime = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
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
        { ".zip",  "application/zip" }
        // Add more mappings as required.
    };

    // Returns true if the supplied MIME type matches the expected type for the file's extension.
    private static bool IsMimeTypeValid(string filePath, string suppliedMime)
    {
        string ext = Path.GetExtension(filePath);
        if (string.IsNullOrEmpty(ext))
            return false; // No extension – cannot validate.

        if (!ExtensionToMime.TryGetValue(ext, out string expectedMime))
            return false; // Unknown extension – treat as invalid.

        return string.Equals(expectedMime, suppliedMime, StringComparison.OrdinalIgnoreCase);
    }

    static void Main()
    {
        const string pdfPath        = "input.pdf";          // PDF to which the attachment will be added
        const string attachmentPath = "sample.docx";        // File to attach
        const string attachmentMime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        const string outputPdfPath  = "output_with_attachment.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment not found: {attachmentPath}");
            return;
        }

        // Validate MIME type against file extension before insertion.
        if (!IsMimeTypeValid(attachmentPath, attachmentMime))
        {
            Console.Error.WriteLine("MIME type does not match file extension. Attachment will not be added.");
            return;
        }

        // Load the PDF, embed the attachment, and save.
        using (Document doc = new Document(pdfPath))
        {
            // Create a FileSpecification for the attachment.
            var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath))
            {
                Description = $"Attached file: {Path.GetFileName(attachmentPath)}",
                MIMEType    = attachmentMime,
                // The file content is stored in a MemoryStream.
                Contents    = new MemoryStream(File.ReadAllBytes(attachmentPath))
            };

            // Add the file specification to the EmbeddedFiles collection.
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Saved as '{outputPdfPath}'.");
    }
}
