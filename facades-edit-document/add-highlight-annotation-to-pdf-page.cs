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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 3 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Get page 3
            Page page = doc.Pages[3];

            // Define the rectangle that covers the text to be highlighted.
            // Coordinates are (llx, lly, urx, ury) in points.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle.
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Use Aspose.Pdf.Color (cross‑platform) for the highlight color.
                Color = Aspose.Pdf.Color.Yellow,
                // Optional: add a comment that appears in the annotation popup.
                Contents = "Highlighted text"
            };

            // Attach the annotation to the page.
            page.Annotations.Add(highlight);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}