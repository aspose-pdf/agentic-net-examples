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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; select the first page
            Page page = doc.Pages[1];

            // Define the rectangle area for the highlight (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Optional: set the highlight color (yellow)
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Set opacity to 70% (0.7)
            highlight.Opacity = 0.7f;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved highlighted PDF to '{outputPath}'.");
    }
}