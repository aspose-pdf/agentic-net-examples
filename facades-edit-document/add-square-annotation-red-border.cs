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

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Ensure the document has at least 5 pages.
        if (doc.Pages.Count < 5)
        {
            Console.Error.WriteLine("The PDF does not contain a page 5.");
            return;
        }

        // Define the square's rectangle (lower‑left x, y, upper‑right x, y).
        // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity.
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

        // Create a square annotation with a red border and semi‑transparent red fill.
        // Use the constructor that takes the target Page and the rectangle bounds.
        SquareAnnotation square = new SquareAnnotation(doc.Pages[5], rect)
        {
            // Red border.
            Color = Aspose.Pdf.Color.Red,
            // Semi‑transparent red fill.
            InteriorColor = Aspose.Pdf.Color.FromRgb(255, 0, 0),
            Opacity = 0.5f
        };

        // Add the annotation to page 5.
        doc.Pages[5].Annotations.Add(square);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}
