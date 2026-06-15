using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_polyline.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices (points are in page coordinate space)
            Aspose.Pdf.Point[] vertices = new Aspose.Pdf.Point[]
            {
                new Aspose.Pdf.Point(120, 520),
                new Aspose.Pdf.Point(200, 580),
                new Aspose.Pdf.Point(280, 540),
                new Aspose.Pdf.Point(360, 600)
            };

            // Create the PolylineAnnotation with the page, rectangle, and vertices
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Blue,          // Stroke color of the polyline
                Contents = "Custom polyline diagram",   // Tooltip text
                Opacity = 0.8f                          // Semi‑transparent
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}