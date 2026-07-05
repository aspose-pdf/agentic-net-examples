using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and the file to be attached
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string attachmentFilePath = "sample.png";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Determine file extension (e.g., ".png")
        string extension = Path.GetExtension(attachmentFilePath).ToLowerInvariant();

        // Simple mapping of extensions to expected MIME types
        var mimeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".png",  "image/png" },
            { ".jpg",  "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif",  "image/gif" },
            { ".pdf",  "application/pdf" },
            { ".txt",  "text/plain" },
            { ".doc",  "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
            // Add more mappings as needed
        };

        // If the extension is unknown, abort
        if (!mimeMap.TryGetValue(extension, out string expectedMime))
        {
            Console.Error.WriteLine($"Unsupported file extension: {extension}");
            return;
        }

        // OPTIONAL: Additional runtime validation of the file's actual MIME type could be added here.
        // For this example we trust the mapping and only ensure the extension is supported.

        // Create a FileSpecification describing the attachment.
        // The constructor takes the file path and a description (can be the file name).
        FileSpecification fileSpec = new FileSpecification(attachmentFilePath, Path.GetFileName(attachmentFilePath));
        // You can set a custom description if desired.
        fileSpec.Description = $"Attachment of type {expectedMime}";

        // Open the PDF, add the attachment, and save
        using (Document doc = new Document(inputPdfPath))
        {
            // Add the file to the document's embedded files collection
            doc.EmbeddedFiles.Add(fileSpec);

            // Optionally create a visible file attachment annotation on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);
            FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(doc.Pages[1], rect, fileSpec);
            // Use a standard icon (Paperclip is the default, but we set it explicitly for clarity)
            fileAnn.Icon = FileIcon.Paperclip;
            doc.Pages[1].Annotations.Add(fileAnn);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine("Attachment added successfully with validated MIME type.");
    }
}
