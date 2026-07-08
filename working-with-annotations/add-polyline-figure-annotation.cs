using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "polyline_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices (points are in page coordinate space)
            Point[] vertices = new Point[]
            {
                new Point(120, 520),
                new Point(200, 580),
                new Point(280, 540),
                new Point(360, 600)
            };

            // Create the PolylineAnnotation with the page, rectangle, and vertices
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Optional appearance settings
                Color = Aspose.Pdf.Color.Blue,          // Stroke color of the polyline
                Contents = "Custom polyline diagram",   // Tooltip text
                Opacity = 0.8,                          // Semi‑transparent
                // Intent can be set if needed, e.g., PolyIntent.PolyLineDimension
                // Intent = PolyIntent.PolyLineDimension
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PolyLine annotation added and saved to '{outputPath}'.");
    }
}