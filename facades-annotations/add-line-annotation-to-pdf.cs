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

        // Ensure at least one page exists.
        if (doc.Pages.Count < 1)
            doc.Pages.Add();

        // Page 1 (Aspose.Pdf pages are 1‑based).
        Page page = doc.Pages[1];

        // Define the rectangle that will host the line annotation.
        // (llx, lly, urx, ury) in points (1/72 inch).
        Rectangle rect = new Rectangle(100, 600, 300, 700);

        // Create a line annotation using the constructor that accepts the page,
        // the bounding rectangle, and the start/end points.
        LineAnnotation line = new LineAnnotation(
            page,
            rect,
            new Point(100, 600),   // start point
            new Point(300, 700)    // end point
        );
        // No additional styling is required – defaults are a black line, width 1, no arrowheads.

        // Add the annotation to the page.
        page.Annotations.Add(line);

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Line annotation added and saved to '{outputPath}'.");
    }
}
