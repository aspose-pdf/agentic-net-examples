using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string fileToAttach  = "attachment.txt";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fileToAttach))
        {
            Console.Error.WriteLine($"File to attach not found: {fileToAttach}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the attachment annotation will be placed (first page)
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that represents the annotation's border
            // (left, bottom, right, top) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a FileSpecification for the file to be embedded
            FileSpecification fileSpec = new FileSpecification(fileToAttach);

            // Create the FileAttachmentAnnotation with the page, rectangle, and file spec
            FileAttachmentAnnotation fileAttachment = new FileAttachmentAnnotation(page, rect, fileSpec)
            {
                // Optional: set an icon (Paperclip, Graph, PushPin, Tag) and a tooltip
                Icon = FileIcon.Paperclip,
                Contents = "Attached file: " + Path.GetFileName(fileToAttach),
                Title = "File Attachment"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(fileAttachment);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with attachment: {outputPdfPath}");
    }
}
