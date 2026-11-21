using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rounded_rect.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle bounds (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a SquareAnnotation which can represent a rectangle with rounded corners
            SquareAnnotation roundedRect = new SquareAnnotation(doc.Pages[1], rect)
            {
                // Border colour (stroke)
                Color = Aspose.Pdf.Color.Black,
                // Fill colour (solid fill)
                InteriorColor = Aspose.Pdf.Color.LightBlue
            };

            // Set border width and corner radius (both horizontal and vertical)
            roundedRect.Border = new Border(roundedRect)
            {
                Width = 1,
                HCornerRadius = 20,
                VCornerRadius = 20
            };

            // Add the annotation to the first page
            doc.Pages[1].Annotations.Add(roundedRect);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with rounded rectangle to '{outputPath}'.");
    }
}
