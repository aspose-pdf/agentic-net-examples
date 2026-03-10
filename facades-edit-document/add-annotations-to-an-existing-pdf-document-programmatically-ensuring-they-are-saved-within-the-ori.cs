using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing; // for Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // Existing PDF to modify
        const string outputPath = "output.pdf"; // Path where the updated PDF will be saved

        // Ensure the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Work on the first page (Aspose.Pdf pages are 1‑based)
        Page page = pdfDocument.Pages[1];

        // ------------------------------------------------------------
        // Add a Text (Sticky Note) annotation on page 1
        // Rectangle(left, bottom, right, top) – coordinates are in points
        // ------------------------------------------------------------
        Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
        TextAnnotation textAnnotation = new TextAnnotation(page, textRect)
        {
            Title    = "Sample Note",
            Contents = "This is a sample sticky note added via Aspose.Pdf.",
            Open     = true,               // Annotation window opened by default
            Name     = "Note1",            // Unique name for the annotation
            Color    = Color.Yellow         // Optional visual cue
        };
        page.Annotations.Add(textAnnotation);

        // ------------------------------------------------------------
        // Add a Web Link annotation on page 1
        // ------------------------------------------------------------
        Aspose.Pdf.Rectangle linkRect = new Aspose.Pdf.Rectangle(100, 400, 300, 420);
        LinkAnnotation linkAnnotation = new LinkAnnotation(page, linkRect)
        {
            Action = new GoToURIAction("https://www.example.com")
        };
        page.Annotations.Add(linkAnnotation);

        // Save the modified PDF. This overwrites the original file if the same path is used.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}
