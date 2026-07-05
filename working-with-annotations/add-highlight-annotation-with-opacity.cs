using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "highlighted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Select the page to annotate (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a HighlightAnnotation with custom color and opacity
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color   = Aspose.Pdf.Color.Yellow, // visual highlight color
                Opacity = 0.5                       // opacity value between 0 (transparent) and 1 (opaque)
            };

            // Attach the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF (lifecycle rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}