using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    // Simple DTO to hold attachment information
    class AttachmentInfo
    {
        public string FilePath { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty; // retained for reference, not used by Aspose API
        public string Description { get; set; } = string.Empty;
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "output_with_attachments.pdf";

        // Define the files to attach together with their MIME types and descriptions
        var attachments = new List<AttachmentInfo>
        {
            new AttachmentInfo
            {
                FilePath    = "document1.docx",
                MimeType    = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                Description = "Word document containing project overview"
            },
            new AttachmentInfo
            {
                FilePath    = "image1.png",
                MimeType    = "image/png",
                Description = "Diagram of the system architecture"
            },
            new AttachmentInfo
            {
                FilePath    = "data.csv",
                MimeType    = "text/csv",
                Description = "Exported data set"
            }
        };

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Verify that each attachment file exists before proceeding
        foreach (var att in attachments)
        {
            if (!File.Exists(att.FilePath))
            {
                Console.Error.WriteLine($"Attachment file not found: {att.FilePath}");
                return;
            }
        }

        // Load the PDF, attach files, and save the result
        using (Document doc = new Document(inputPdfPath))
        {
            // Use the first page for the attachment annotations (1‑based indexing)
            Page page = doc.Pages[1];

            // Position each annotation slightly offset so they don't overlap
            double left = 50;
            double bottom = 750;
            double width = 20;
            double height = 20;
            const double verticalSpacing = 30;

            foreach (var att in attachments)
            {
                // Create a FileSpecification using the constructor that accepts file path and description
                FileSpecification fileSpec = new FileSpecification(att.FilePath, att.Description);
                // The Description property can be set again if needed
                fileSpec.Description = att.Description;

                // Define the rectangle for the annotation
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                    left,
                    bottom,
                    left + width,
                    bottom + height);

                // Create the file attachment annotation
                FileAttachmentAnnotation fileAnn = new FileAttachmentAnnotation(page, rect, fileSpec)
                {
                    // Optional visual settings
                    Icon = FileIcon.PushPin,
                    Color = Aspose.Pdf.Color.Blue,
                    Title = Path.GetFileName(att.FilePath) // shown in the popup title bar
                };

                // Add the annotation to the page
                page.Annotations.Add(fileAnn);

                // Move the next annotation downwards
                bottom -= verticalSpacing;
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachments: {outputPdfPath}");
    }
}
