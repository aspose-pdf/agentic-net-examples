using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_dashed_rectangle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation
            // Coordinates: lower‑left (llx, lly) and upper‑right (urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (rectangle) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Yellow,   // Fill color of the rectangle
                Contents = "Dashed border rectangle"
            };

            // Configure the border: dashed style with custom dash pattern
            // Border constructor requires the parent annotation
            Border border = new Border(square)
            {
                Width = 2,                                 // Border thickness
                Style = BorderStyle.Dashed,                // Dashed border style
                Dash = new Dash(5, 3)                      // 5 units dash, 3 units gap
            };
            square.Border = border;

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dashed rectangle annotation to '{outputPath}'.");
    }
}