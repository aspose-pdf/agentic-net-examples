using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "CircleCreated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the bounding rectangle for the circle (center 250,250 radius 50)
            // Lower‑left corner (200,200), upper‑right corner (300,300)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(200, 200, 300, 300);

            // Create the circle annotation
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Border color (stroke)
                Color = Aspose.Pdf.Color.Red,
                // Fill color – use InteriorColor for figure annotations
                InteriorColor = Aspose.Pdf.Color.Red,
                // Opacity (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.6f,
                // Optional: set a title and contents for accessibility
                Title    = "Red Circle",
                Contents = "A red circle annotation centered at (250,250) with radius 50."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(circle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}
