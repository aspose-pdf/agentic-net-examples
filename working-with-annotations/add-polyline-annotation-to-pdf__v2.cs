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

        // Define the polyline vertices
        List<Aspose.Pdf.Point> points = new List<Aspose.Pdf.Point>
        {
            new Aspose.Pdf.Point(100, 700),
            new Aspose.Pdf.Point(150, 750),
            new Aspose.Pdf.Point(200, 720),
            new Aspose.Pdf.Point(250, 770)
        };

        // Convert the list to an array required by the constructor
        Aspose.Pdf.Point[] vertices = points.ToArray();

        // Compute a bounding rectangle that encloses all vertices (with a small padding)
        double llx = double.MaxValue, lly = double.MaxValue, urx = double.MinValue, ury = double.MinValue;
        foreach (var p in vertices)
        {
            if (p.X < llx) llx = p.X;
            if (p.Y < lly) lly = p.Y;
            if (p.X > urx) urx = p.X;
            if (p.Y > ury) ury = p.Y;
        }
        const double padding = 10;
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
            llx - padding,
            lly - padding,
            urx + padding,
            ury + padding);

        // Load the PDF, add the annotation, and save
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create the polyline annotation using the constructor that accepts page, rectangle, and vertices
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, vertices)
            {
                Color = Aspose.Pdf.Color.Blue,   // Set the line color
                Contents = "Sample polyline"      // Optional tooltip text
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Polyline annotation added and saved to '{outputPath}'.");
    }
}