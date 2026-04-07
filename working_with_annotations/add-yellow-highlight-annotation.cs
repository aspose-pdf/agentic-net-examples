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

        // Load the PDF, modify it, and save – all within using blocks for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to annotate
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the highlight annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation on the selected page
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the highlight color to yellow
                Color = Aspose.Pdf.Color.Yellow,
                // Set opacity to 80% (0.8)
                Opacity = 0.8
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}