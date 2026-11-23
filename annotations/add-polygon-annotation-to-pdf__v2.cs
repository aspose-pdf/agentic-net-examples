using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PolygonRegion.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to place the annotation (first page)
            Page page = doc.Pages[1];

            // Define the polygon vertices (example rectangle shape)
            var vertices = new List<Point>
            {
                new Point(100, 500),
                new Point(200, 500),
                new Point(200, 600),
                new Point(100, 600)
            };

            // Bounding rectangle for the polygon annotation
            var rect = new Rectangle(100, 500, 200, 600);

            // PolygonAnnotation does not expose a PageNumber property. Use the constructor that
            // receives the target Page and a bounding Rectangle.
            PolygonAnnotation polygon = new PolygonAnnotation(page, rect, vertices.ToArray())
            {
                Title = "Region",
                Color = Color.FromRgb(0.0, 0.5, 0.5),          // teal border
                InteriorColor = Color.FromRgb(0.0, 1.0, 1.0), // cyan fill
                Opacity = 0.5
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polygon);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polygon annotation added and saved to '{outputPath}'.");
    }
}
