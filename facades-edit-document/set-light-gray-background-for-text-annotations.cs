using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // TextAnnotation, TextIcon, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);
        // Work on the first page (pages are 1‑based).
        Page page = doc.Pages[1];

        // Define the annotation rectangle (llx, lly, urx, ury) – coordinates are in points.
        var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Create a TextAnnotation with the desired properties.
        var textAnnotation = new TextAnnotation(page, rect)
        {
            Title = "Note",
            Contents = "This annotation has a light‑gray background for readability on dark pages.",
            Icon = TextIcon.Note,
            Open = true,
            // Apply a light gray background for better readability on dark pages.
            Color = Aspose.Pdf.Color.LightGray // use Annotation.Color instead of non‑existent BackgroundColor
        };

        // Add the annotation to the page.
        page.Annotations.Add(textAnnotation);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
