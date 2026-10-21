using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";
        const int pageNumber = 1; // target page (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // 2 mm → points (1 pt = 1/72 in, 1 in = 25.4 mm)
        const float mmToPt = 72f / 25.4f;
        float borderThicknessPt = 2f * mmToPt; // ≈5.67 pt
        // Border.Width expects an integer (points). Round to the nearest whole point.
        int borderWidth = (int)Math.Round(borderThicknessPt);

        // Load the PDF document.
        Document doc = new Document(inputPath);

        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Invalid page number {pageNumber}. Document has {doc.Pages.Count} pages.");
            return;
        }

        // Define rectangle coordinates (llx, lly, urx, ury) in points.
        // Adjust these values as needed for your PDF.
        Rectangle rect = new Rectangle(100, 100, 200, 200);

        // Create a square (rectangle) annotation.
        SquareAnnotation square = new SquareAnnotation(doc.Pages[pageNumber], rect);
        square.Color = Color.Red;
        // Set border thickness (integer value).
        square.Border = new Border(square) { Width = borderWidth };

        // Add the annotation to the page.
        doc.Pages[pageNumber].Annotations.Add(square);

        // Save the modified document.
        doc.Save(outputPath);

        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}
