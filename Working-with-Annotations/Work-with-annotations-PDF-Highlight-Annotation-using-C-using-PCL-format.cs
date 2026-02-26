using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle area to be highlighted (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation on the selected page
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Use cross‑platform Aspose.Pdf.Color
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.5,
                // Optional comment displayed in the annotation popup
                Contents = "Important passage"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}