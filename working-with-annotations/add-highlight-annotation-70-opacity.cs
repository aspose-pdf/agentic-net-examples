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

        // Load the PDF document within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page to which the annotation will be added (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the highlight (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect);

            // Set the opacity to 70% (0.7). This makes the highlight subtle.
            highlight.Opacity = 0.7f;

            // Optional: set the highlight color (yellow is typical for highlights)
            highlight.Color = Aspose.Pdf.Color.Yellow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation with 70% opacity saved to '{outputPath}'.");
    }
}