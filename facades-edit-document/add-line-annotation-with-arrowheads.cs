using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define a rectangle that loosely bounds the line (required by the constructor)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Define the start and end points of the line
            Point start = new Point(100, 500);
            Point end   = new Point(300, 400);

            // Create the line annotation using the (Page, Rectangle, Point, Point) overload
            LineAnnotation line = new LineAnnotation(page, rect, start, end);

            // Set arrowheads at both ends of the line
            line.StartingStyle = LineEnding.OpenArrow;
            line.EndingStyle   = LineEnding.OpenArrow;

            // Optional: set the line color (use Aspose.Pdf.Color, not System.Drawing.Color)
            line.Color = Aspose.Pdf.Color.Black;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line annotation with arrowheads saved to '{outputPath}'.");
    }
}