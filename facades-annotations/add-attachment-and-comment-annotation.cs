using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string sourcePdfPath = "source.pdf";
        const string attachmentFilePath = "attachment.txt";
        const string outputPdfPath = "documented.pdf";

        // Verify required files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        // Load the PDF document using the high‑level Aspose.Pdf API
        Document pdf = new Document(sourcePdfPath);

        // ------------------------------------------------------------
        // 1. Add a file attachment (appears in the attachment panel)
        // ------------------------------------------------------------
        // Use Document.EmbeddedFiles with a FileSpecification – the Attachments
        // collection and FileAttachment class do not exist in Aspose.Pdf.
        var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
        {
            Description = "Sample attachment",
            Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath))
        };
        pdf.EmbeddedFiles.Add(fileSpec);

        // ------------------------------------------------------------
        // 2. Add a comment (sticky‑note) annotation on page 1
        // ------------------------------------------------------------
        // A TextAnnotation is the typical "comment" annotation.
        // It needs a rectangle that defines the clickable area.
        // Coordinates are in points (1/72 inch) measured from the lower‑left corner.
        // Here we create a small 20×20 rectangle at (100,200).
        var commentRect = new Aspose.Pdf.Rectangle(100, 200, 120, 220);
        var comment = new TextAnnotation(pdf.Pages[1], commentRect)
        {
            Title = "Comment",
            Contents = "This is a comment annotation."
        };
        pdf.Pages[1].Annotations.Add(comment);

        // Save the modified PDF
        pdf.Save(outputPdfPath);

        Console.WriteLine($"PDF with attachment and comment saved to '{outputPdfPath}'.");
    }
}
