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

        using (Document doc = new Document(inputPath))
        {
            // Select the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle for the highlight (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Yellow,
                Contents = "Important text highlighted",
                Opacity = 0.5
            };

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added and saved to '{outputPath}'.");
    }
}