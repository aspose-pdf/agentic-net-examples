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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to place the annotation (first page, 1‑based index)
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

            // Create the PolylineAnnotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                // Optional visual properties
                Color = Aspose.Pdf.Color.Blue,          // Stroke color of the polyline
                Contents = "Custom diagram polyline",   // Tooltip text
                Opacity = 0.8f                          // Slight transparency
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with polyline annotation: {outputPath}");
    }
}