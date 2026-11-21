using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_dashed_rectangle.pdf";

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

            // Define the rectangle area (lower‑left X/Y, upper‑right X/Y)
            // Coordinates are in points (1/72 inch). Adjust as needed.
            Rectangle rect = new Rectangle(100, 500, 300, 600);

            // Border properties
            Color borderColor = Color.Blue;
            int borderWidth = 2; // Border.Width expects an integer
            BorderStyle borderStyle = BorderStyle.Dashed;

            // Create a square (rectangle) annotation
            var square = new SquareAnnotation(page, rect)
            {
                Color = borderColor
            };

            // Configure the border (width and style). Pass the annotation itself to the Border constructor.
            square.Border = new Border(square)
            {
                Width = borderWidth,
                Style = borderStyle
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Dashed rectangle annotation added. Saved to '{outputPath}'.");
    }
}
