using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Get page six (1‑based indexing)
            Page page = doc.Pages[6];

            // Define the rectangle area for the annotation
            Rectangle rect = new Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on the specified page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Color.Blue,          // Border color
                Opacity = 0.5f               // 50% opacity
            };

            // Configure a dashed border (must be set after the annotation is created)
            Border border = new Border(square)
            {
                Width = 2,                                 // Border width in points
                Style = BorderStyle.Dashed,                // Dashed style (enum)
                Dash = new Dash(new int[] { 3, 2 })       // Dash pattern: 3 points on, 2 points off
            };
            square.Border = border;

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}
