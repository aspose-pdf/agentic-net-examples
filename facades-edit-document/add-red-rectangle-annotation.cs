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
        const int pageNumber = 2; // 1‑based page index

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Validate page number
        if (pageNumber < 1 || pageNumber > doc.Pages.Count)
        {
            Console.Error.WriteLine($"Page {pageNumber} does not exist in the document (has {doc.Pages.Count} pages).");
            return;
        }

        // Define rectangle (lower‑left x,y and upper‑right x,y) in points
        var rect = new Rectangle(100, 500, 300, 700);

        // Red border color (Aspose.Pdf.Color, not System.Drawing.Color)
        var borderColor = Aspose.Pdf.Color.Red;

        // Border thickness: 2 mm ≈ 5.669 points → round to nearest integer (int required)
        int borderWidth = 6;

        // Create the square (rectangle) annotation
        var square = new SquareAnnotation(doc.Pages[pageNumber], rect)
        {
            Color = borderColor
        };

        // Border must be created with the owning annotation as the required parent argument
        square.Border = new Border(square)
        {
            Width = borderWidth,          // int value
            Style = BorderStyle.Solid    // optional, defaults to Solid
        };

        // Add the annotation to the page's annotation collection
        doc.Pages[pageNumber].Annotations.Add(square);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Red rectangle annotation added to page {pageNumber} and saved as '{outputPath}'.");
    }
}
