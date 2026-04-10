using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF to which files will be attached
        const string inputPdfPath = "input.pdf";
        // Output PDF with attachments
        const string outputPdfPath = "output_with_attachments.pdf";

        // Define files to attach: each entry contains the file path, MIME type and description
        // NOTE: MIME type is kept for documentation purposes only – Aspose.Pdf does not expose a settable MimeType property.
        var attachments = new List<(string FilePath, string MimeType, string Description)>
        {
            ("document1.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Word document attachment"),
            ("image1.png",    "image/png",  "Sample PNG image"),
            ("data.csv",      "text/csv",   "CSV data file")
        };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure all attachment files exist before proceeding
        foreach (var (filePath, _, _) in attachments)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"Attachment file not found: {filePath}");
                return;
            }
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // We'll place all attachments on the first page; adjust as needed
            Page page = doc.Pages[1];

            // Define a rectangle for the annotation icon (position and size)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            foreach (var (filePath, _, description) in attachments)
            {
                // Create a FileSpecification using the constructor that accepts the file path and description.
                // The Name property (display name) is automatically derived from the file name.
                FileSpecification fileSpec = new FileSpecification(filePath, description);
                // Ensure the description is also set (the constructor already does this, but we keep it explicit).
                fileSpec.Description = description;

                // Create the file attachment annotation on the page.
                FileAttachmentAnnotation attachment = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Set the icon that represents the attachment. Use the FileIcon enum.
                    Icon = FileIcon.Paperclip,
                    // Optional tooltip text when hovering over the icon.
                    Contents = description,
                    // Subject can also hold a short description.
                    Subject = description
                };

                // Add the annotation to the page's annotation collection.
                page.Annotations.Add(attachment);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}
