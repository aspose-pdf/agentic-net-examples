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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (document lifecycle handled via using)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (position and size on the page)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 200);

            // Define the polyline vertices (points are in page coordinate space)
            // Points are defined as Aspose.Pdf.Point
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
                Opacity = 0.8,                          // Semi‑transparent
                Contents = "Custom diagram polyline",   // Tooltip text
                Title = "Diagram Polyline"              // Title shown in the annotation popup
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified PDF (document lifecycle handled via using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PolyLine annotation added and saved to '{outputPath}'.");
    }
}