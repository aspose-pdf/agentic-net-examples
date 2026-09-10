using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Ensure the document has at least three pages.
        if (pdfDocument.Pages.Count < 3)
        {
            Console.Error.WriteLine("The PDF does not contain a page 3.");
            return;
        }

        // Define the annotation rectangle (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        // Rectangle: x = 100, y = 500, width = 200, height = 100
        var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Create a FileSpecification for the attachment.
        var fileSpec = new FileSpecification("sample.txt", "Sample attachment");

        // Create a file attachment annotation with 60% opacity.
        var fileAttachment = new FileAttachmentAnnotation(pdfDocument.Pages[3], rect, fileSpec)
        {
            Opacity = 0.6f,                     // Semi‑transparent
            Icon = FileIcon.Paperclip,          // Valid icon enum
            Contents = "Sample attachment"      // Tooltip / contents
        };

        // Add the annotation to page 3.
        pdfDocument.Pages[3].Annotations.Add(fileAttachment);

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Annotation added with opacity 0.6 on page 3. Saved to '{outputPath}'.");
    }
}
