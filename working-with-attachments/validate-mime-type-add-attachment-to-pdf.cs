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
        { ".txt",  "text/plain" },
        { ".pdf",  "application/pdf" },
        { ".jpg",  "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png",  "image/png" },
        { ".gif",  "image/gif" },
        { ".doc",  "application/msword" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xls",  "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".ppt",  "application/vnd.ms-powerpoint" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        // add more as needed
    };

    static string GetMimeTypeFromExtension(string filePath)
    {
        string ext = Path.GetExtension(filePath);
        if (ext != null && MimeMap.TryGetValue(ext, out string mime))
            return mime;
        return "application/octet-stream"; // fallback
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string attachmentPath = "sample.txt";

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
        string expectedMime = GetMimeTypeFromExtension(attachmentPath);

        // Create FileSpecification for the attachment
        FileSpecification fileSpec;
        using (FileStream fs = File.OpenRead(attachmentPath))
        {
            // Path.GetFileName never returns null, but the compiler cannot guarantee it.
            // Use the null‑forgiving operator to silence CS8600.
            fileSpec = new FileSpecification(fs, Path.GetFileName(attachmentPath)!);
        }

        // Set the MIME type on the specification
        fileSpec.MIMEType = expectedMime;

        // Validate MIME type against extension (simple check)
        if (!string.Equals(expectedMime, fileSpec.MIMEType, StringComparison.OrdinalIgnoreCase))
        {
            Console.Error.WriteLine("MIME type does not match file extension. Attachment will not be added.");
            return;
        }

        // Open the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Add the file specification to the embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Optionally, create a visible file attachment annotation on the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 150, 550);
            FileAttachmentAnnotation attachmentAnnot = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // The Icon enum is not available in the referenced Aspose.PDF version; the default icon (Paperclip) will be used.
                // Icon = FileAttachmentAnnotation.FileAttachmentIcon.Paperclip,
                Contents = "Attached file: " + Path.GetFileName(attachmentPath),
                Title = "Attachment"
            };
            page.Annotations.Add(attachmentAnnot);

            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Attachment added and PDF saved to '{outputPdfPath}'.");
    }
}
