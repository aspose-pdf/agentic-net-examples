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

        // Define the rectangle that covers the text to be highlighted on page 3.
        // Coordinates are in points (1/72 inch) with origin at the lower‑left corner.
        double llx = 100; // lower‑left X
        double lly = 500; // lower‑left Y
        double urx = 300; // upper‑right X
        double ury = 520; // upper‑right Y
        // Use Aspose.Pdf.Rectangle (annotation bounds), not System.Drawing.Rectangle.
        Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // Get the target page (pages collection is 1‑based).
            Page page = pdfDoc.Pages[3];

            // Create a highlight annotation. The constructor requires the page and the rectangle.
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                // Optional visual properties. Use Aspose.Pdf.Color to avoid ambiguity.
                Color = Aspose.Pdf.Color.Yellow,
                Title = "Highlight",
                Contents = "Highlighted text"
            };

            // Add the annotation to the page.
            page.Annotations.Add(highlight);

            // Save the modified PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}
