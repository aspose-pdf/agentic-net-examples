using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page to annotate (first page, 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to highlight (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a highlight annotation on the specified rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set highlight color (yellow) and custom opacity (50% transparent)
            highlight.Color = Aspose.Pdf.Color.Yellow;
            highlight.Opacity = 0.5f;

            // Attach the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}