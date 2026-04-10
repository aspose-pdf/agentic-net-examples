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

        // Load the PDF inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing.
            Page firstPage = doc.Pages[1];

            // Define the rectangle area (llx, lly, urx, ury) in user space units.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on the first page.
            SquareAnnotation rectangleAnnotation = new SquareAnnotation(firstPage, rect)
            {
                // Set the border colour.
                Color = Aspose.Pdf.Color.Blue,
                // Set the annotation opacity to 75 % (0.75).
                Opacity = 0.75
            };

            // Configure a custom dash pattern for the border.
            // Border requires the parent annotation in its constructor.
            Border border = new Border(rectangleAnnotation)
            {
                Width = 2,                     // Border width in points.
                Dash = new Dash(new int[] { 3, 2 }) // 3‑point dash, 2‑point gap.
            };
            rectangleAnnotation.Border = border;

            // Add the annotation to the page's annotation collection.
            firstPage.Annotations.Add(rectangleAnnotation);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle annotation added and saved to '{outputPath}'.");
    }
}