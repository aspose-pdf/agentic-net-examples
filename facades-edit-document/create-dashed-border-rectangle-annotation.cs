using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_dashed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle bounds for the annotation
            // Parameters: left, bottom, right, top
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a square (rectangle) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Yellow,          // Fill color of the annotation
                Contents = "Dashed border rectangle"      // Optional tooltip text
            };

            // Configure a dashed border for the annotation
            // Border constructor requires the parent annotation instance
            Border border = new Border(square)
            {
                Style = BorderStyle.Dashed,   // Use the dashed border style
                Width = 2,                    // Border width in points
                Dash = new Dash(3, 2)         // Dash pattern: 3 points on, 2 points off
            };
            square.Border = border;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified PDF (format inferred from the file extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with dashed rectangle annotation to '{outputPath}'.");
    }
}