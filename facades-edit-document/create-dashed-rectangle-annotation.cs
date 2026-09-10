using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_dashed_rect.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdf = new Document(inputPath);

        // Define rectangle bounds (lower‑left X/Y and upper‑right X/Y) in points.
        int x = 100;
        int y = 500;
        int width = 200;
        int height = 100;
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(x, y, x + width, y + height);

        // Create a square (rectangle) annotation on the first page.
        SquareAnnotation square = new SquareAnnotation(pdf.Pages[1], rect)
        {
            Contents = "Dashed rectangle",          // Tooltip text
            Color = Aspose.Pdf.Color.Blue           // Border color
        };

        // Configure a dashed border: width = 2 points, dash pattern = {3,2}.
        // In recent Aspose.PDF versions the BorderStyle property was removed.
        // A dashed appearance is achieved by setting the Dash array.
        square.Border = new Border(square)
        {
            Width = 2
        };
        square.Border.Dash = new Dash(new int[] { 3, 2 });

        // Add the annotation to the page.
        pdf.Pages[1].Annotations.Add(square);

        // Save the modified PDF.
        pdf.Save(outputPath);

        Console.WriteLine($"Dashed rectangle annotation saved to '{outputPath}'.");
    }
}
