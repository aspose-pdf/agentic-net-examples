using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "colored_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle where the square annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a square annotation on the specified page
            SquareAnnotation square = new SquareAnnotation(page, rect);

            // Set the border color of the annotation
            square.Color = Aspose.Pdf.Color.Red;

            // Set the interior (fill) color of the annotation
            square.InteriorColor = Aspose.Pdf.Color.Yellow;

            // Optional: make the annotation semi‑transparent
            square.Opacity = 0.5;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with colored annotation to '{outputPath}'.");
    }
}