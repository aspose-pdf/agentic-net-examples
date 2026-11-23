using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PolylineRoute.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document has no pages.");
                return;
            }

            // Choose the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define a rectangle that roughly bounds the polyline (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

            // Define the vertices of the polyline
            Point[] vertices = new Point[]
            {
                new Point(120, 480),
                new Point(200, 420),
                new Point(280, 350),
                new Point(360, 300)
            };

            // Create the polyline annotation with the required vertices parameter
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Title   = "Route",
                // Orange color (RGB: 255,165,0)
                Color   = Aspose.Pdf.Color.FromRgb(255, 165, 0),
                Opacity = 0.7f
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}
