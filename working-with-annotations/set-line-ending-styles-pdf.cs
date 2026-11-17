using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the line annotation rectangle (coordinates are in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Create a line annotation – constructor requires the page, rectangle, start point and end point
            LineAnnotation line = new LineAnnotation(
                page,
                rect,
                new Aspose.Pdf.Point(100, 500),   // start point
                new Aspose.Pdf.Point(300, 400)    // end point
            )
            {
                // Set the line color (cross‑platform Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.Blue,
                // Optional: set the line width
                Width = 2
            };

            // Set line ending styles: circle at the start, closed arrow at the end
            line.StartingStyle = LineEnding.Circle;
            line.EndingStyle   = LineEnding.ClosedArrow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
