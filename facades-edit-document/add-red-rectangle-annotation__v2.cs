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
        const int targetPageNumber = 1; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // Get the target page
            Page page = doc.Pages[targetPageNumber];

            // Define the rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a square (rectangle) annotation on the page
            SquareAnnotation square = new SquareAnnotation(page, rect);
            // Set the border color to red
            square.Color = Aspose.Pdf.Color.Red;

            // 2 mm ≈ 5.6693 points. Border.Width expects an int, so round to the nearest integer.
            const float mmToPoints = 72f / 25.4f; // conversion factor
            float borderWidthPoints = 2f * mmToPoints; // 2 mm in points
            int borderWidthInt = (int)Math.Round(borderWidthPoints);
            square.Border = new Border(square) { Width = borderWidthInt };

            // Add the annotation to the page
            page.Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Red rectangle annotation added and saved to '{outputPath}'.");
    }
}
