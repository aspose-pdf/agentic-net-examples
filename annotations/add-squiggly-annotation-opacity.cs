using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "SquigglyAgreement.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SquigglyAnnotation and set its opacity (HighlightSettings does not exist for SquigglyAnnotation)
            SquigglyAnnotation squiggly = new SquigglyAnnotation(page, rect)
            {
                Opacity = 0.7f // 70% opacity
                // Optional: set a color for the squiggly line
                // Color = Color.Yellow
            };

            // Add the annotation to the page
            page.Annotations.Add(squiggly);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Squiggly annotation added and saved to '{outputPath}'.");
    }
}
