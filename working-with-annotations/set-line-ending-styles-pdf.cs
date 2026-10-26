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

        using (Document doc = new Document(inputPath))
        {
            // Get the first page where the annotation will be placed
            Page page = doc.Pages[1];

            // Define the bounding rectangle for the annotation (coordinates in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 400);

            // Create a line (figure) annotation with explicit start and end points
            LineAnnotation line = new LineAnnotation(
                page,
                rect,
                new Aspose.Pdf.Point(100, 500),   // start point (matches lower‑left of rect)
                new Aspose.Pdf.Point(300, 400)    // end point (matches upper‑right of rect)
            )
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue
            };

            // Set line ending styles: circle at start, open arrow at end
            line.StartingStyle = LineEnding.Circle;
            line.EndingStyle   = LineEnding.OpenArrow;

            // Add the annotation to the page
            page.Annotations.Add(line);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
