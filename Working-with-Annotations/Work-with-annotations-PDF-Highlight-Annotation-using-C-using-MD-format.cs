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
            // Verify the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("Document contains no pages.");
                return;
            }

            // Select the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area to highlight (coordinates in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set visual properties of the annotation
            highlight.Color = Aspose.Pdf.Color.Yellow; // highlight color
            highlight.Opacity = 0.5;                    // semi‑transparent
            highlight.Contents = "Important text";      // optional popup text

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}