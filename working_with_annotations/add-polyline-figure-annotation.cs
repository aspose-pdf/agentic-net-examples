using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "polyline_annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Define the polyline vertices
        List<Aspose.Pdf.Point> vertices = new List<Aspose.Pdf.Point>
        {
            new Aspose.Pdf.Point(100, 700),
            new Aspose.Pdf.Point(150, 650),
            new Aspose.Pdf.Point(200, 720),
            new Aspose.Pdf.Point(250, 680),
            new Aspose.Pdf.Point(300, 730)
        };

        // Compute a simple bounding rectangle that contains all points
        double llx = double.MaxValue, lly = double.MaxValue;
        double urx = double.MinValue, ury = double.MinValue;
        foreach (var pt in vertices)
        {
            if (pt.X < llx) llx = pt.X;
            if (pt.Y < lly) lly = pt.Y;
            if (pt.X > urx) urx = pt.X;
            if (pt.Y > ury) ury = pt.Y;
        }
        // Add a small margin so the annotation rectangle is slightly larger than the polyline
        const double margin = 10;
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
            llx - margin, lly - margin,
            urx + margin, ury + margin);

        // Load the PDF, add the polyline annotation, and save
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create the polyline annotation
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices.ToArray())
            {
                // Optional visual styling
                Color = Aspose.Pdf.Color.Blue,
                Opacity = 0.8f,
                Contents = "Sample polyline annotation"
            };

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}