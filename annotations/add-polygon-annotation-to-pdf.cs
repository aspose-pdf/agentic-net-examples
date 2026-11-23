using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "PolygonArea.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (adjust if needed)
            Page page = doc.Pages[1];

            // Define the vertices of the polygon
            Point[] vertices = new Point[]
            {
                new Point(100, 100),
                new Point(150, 200),
                new Point(200, 100)
            };

            // Create the polygon annotation – the constructor expects the Document and the vertices array
            PolygonAnnotation polygon = new PolygonAnnotation(doc, vertices)
            {
                Title = "Area",
                Color = Color.Green // Border color of the annotation
            };

            // Set the bounding rectangle that encloses the polygon points
            polygon.Rect = new Rectangle(100, 100, 200, 200);

            // Attach the annotation to the page
            page.Annotations.Add(polygon);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polygon annotation added and saved to '{outputPath}'.");
    }
}
