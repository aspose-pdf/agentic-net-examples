using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page to annotate (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the text to be highlighted
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);

            // Create a highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set the highlight color (using Aspose.Pdf.Color, not System.Drawing.Color)
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Set custom opacity (0.0 = fully transparent, 1.0 = fully opaque)
            highlight.Opacity = 0.5f; // 50% opacity

            // Add the annotation to the page's Annotations collection
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}