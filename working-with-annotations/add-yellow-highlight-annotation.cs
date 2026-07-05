using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the highlight will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the highlight annotation
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the annotation color to yellow
                Color = Aspose.Pdf.Color.Yellow,
                // Set opacity to 80% (0.8)
                Opacity = 0.8
            };

            // Add the annotation to the page
            page.Annotations.Add(highlight);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Highlight annotation added. Saved to '{outputPath}'.");
    }
}