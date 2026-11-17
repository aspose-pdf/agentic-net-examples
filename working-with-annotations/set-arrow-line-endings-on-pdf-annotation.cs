using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_arrow.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed.
            Page page = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle that bounds the line annotation.
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Create a line annotation with explicit start and end points.
            // The constructor that matches the modern API is:
            // LineAnnotation(Page page, Rectangle rect, Point start, Point end)
            LineAnnotation line = new LineAnnotation(
                page,
                rect,
                new Aspose.Pdf.Point(100, 500), // start point (absolute coordinates)
                new Aspose.Pdf.Point(300, 400)  // end point (absolute coordinates)
            )
            {
                // Arrow at the end to indicate direction.
                EndingStyle = LineEnding.ClosedArrow,
                // No arrow at the start.
                StartingStyle = LineEnding.None,
                // Line colour.
                Color = Color.Black
            };

            // Set line width via the Border property (the modern API uses Border for line thickness).
            line.Border = new Border(line) { Width = 2 };

            // Add the annotation to the page.
            page.Annotations.Add(line);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with arrow line annotation to '{outputPath}'.");
    }
}
