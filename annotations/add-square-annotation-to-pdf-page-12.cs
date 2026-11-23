using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "SquareLabeled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 12 pages (1‑based indexing)
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("The document does not contain page 12.");
                return;
            }

            // Get page 12
            Page page = doc.Pages[12];

            // Define the rectangle for the square annotation (llx, lly, urx, ury)
            // Example coordinates: 100,100 to 200,200
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

            // Create the square annotation
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Title   = "Important",
                Subject = "Review",
                // Navy color (RGB 0,0,0.5)
                Color   = Aspose.Pdf.Color.FromRgb(0, 0, 0.5)
            };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}