using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 6 pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Get page 6 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[6];

            // Define the rectangle area for the annotation (llx, lly, urx, ury)
            Rectangle rect = new Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on the specified page
            SquareAnnotation square = new SquareAnnotation(page, rect);

            // Set 50% opacity
            square.Opacity = 0.5f;

            // Configure a dashed border: width = 2 points, dash pattern = 3 on, 3 off
            // Border class resides in Aspose.Pdf.Annotations and requires the parent annotation
            square.Border = new Border(square)
            {
                Width = 2,
                Dash = new Dash(new int[] { 3, 3 })
            };

            // Optionally set a visible border color (e.g., black)
            square.Color = Color.Black;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
