using System;
using System.Collections.Generic;
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

        // Define the points of the polyline
        List<Point> polylinePoints = new List<Point>
        {
            new Point(100, 700),
            new Point(150, 650),
            new Point(200, 700),
            new Point(250, 650)
        };

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define a rectangle that roughly bounds the polyline (optional)
            Rectangle bounds = new Rectangle(90, 660, 260, 720);

            // Create a PolylineAnnotation with the vertices supplied in the constructor
            PolylineAnnotation polyline = new PolylineAnnotation(page, bounds, polylinePoints.ToArray())
            {
                Color = Aspose.Pdf.Color.Blue, // line color (use Aspose.Pdf.Color, not System.Drawing.Color)
                Opacity = 0.8                 // semi‑transparent
                // Note: Modern Aspose.PDF versions do not expose a LineWidth property for PolylineAnnotation.
                // The default line thickness is used, or you can adjust it via the underlying PDF content if needed.
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}
