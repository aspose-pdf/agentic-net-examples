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

        // Define rectangle coordinates (lower‑left x,y and upper‑right x,y)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least 6 pages
        if (doc.Pages.Count < 6)
        {
            Console.Error.WriteLine("The source PDF does not contain page 6.");
            return;
        }

        // Get page 6 (1‑based indexing)
        Page page = doc.Pages[6];

        // Create a square (rectangle) annotation
        SquareAnnotation squareAnnotation = new SquareAnnotation(page, rect)
        {
            Color = Color.Black,          // border color
            Opacity = 0.5,                // 50% opacity
            Title = "Author",            // tooltip title
            Contents = "Rectangle annotation" // tooltip contents
        };

        // Set dashed border style with width 1 (Border.Width expects an int)
        squareAnnotation.Border = new Border(squareAnnotation)
        {
            Style = BorderStyle.Dashed,
            Width = 1 // integer value
        };

        // Add the annotation to the page
        page.Annotations.Add(squareAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Rectangle annotation added to page 6 and saved as '{outputPath}'.");
    }
}
