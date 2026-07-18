using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string attachmentPath = "sample.png";
        const string outputPdfPath  = "output.pdf";

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

        // Simple MIME type map based on file extension
        var mimeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".pdf",  "application/pdf" },
            { ".png",  "image/png" },
            { ".jpg",  "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif",  "image/gif" },
            { ".txt",  "text/plain" },
            { ".doc",  "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls",  "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" }
            // add more as needed
        };

        string ext = Path.GetExtension(attachmentPath);
        if (!mimeMap.TryGetValue(ext, out string expectedMime))
        {
            Console.Error.WriteLine($"Unsupported file extension: {ext}");
            return;
        }

        // Create a FileSpecification for the attachment and set its MIME type
        FileSpecification fileSpec = new FileSpecification(attachmentPath)
        {
            MIMEType = expectedMime
        };

        // Validate MIME type matches the expected type for the extension
        if (!string.Equals(fileSpec.MIMEType, expectedMime, StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine($"MIME type mismatch: expected {expectedMime}, got {fileSpec.MIMEType}");
            return;
        }

        // Load the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Add the file specification to the embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Create a file attachment annotation on the first page
            Page page = doc.Pages[1];
            // Define a rectangle for the annotation icon
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            FileAttachmentAnnotation attachmentAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // The Icon property defaults to Paperclip; explicit assignment omitted to avoid enum‑resolution issues.
                Contents = $"Attached file: {Path.GetFileName(attachmentPath)}",
                Color = Aspose.Pdf.Color.Blue
            };
            page.Annotations.Add(attachmentAnnot);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
    }
}
