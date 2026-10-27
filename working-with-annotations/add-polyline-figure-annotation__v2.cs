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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Define the points that the polyline will pass through
        List<Aspose.Pdf.Point> points = new List<Aspose.Pdf.Point>
        {
            new Aspose.Pdf.Point(100, 700),
            new Aspose.Pdf.Point(150, 750),
            new Aspose.Pdf.Point(200, 720),
            new Aspose.Pdf.Point(250, 770)
        };

        // Compute a bounding rectangle that encloses all points (with a small margin)
        double llx = double.MaxValue, lly = double.MaxValue, urx = double.MinValue, ury = double.MinValue;
        foreach (var p in points)
        {
            if (p.X < llx) llx = p.X;
            if (p.Y < lly) lly = p.Y;
            if (p.X > urx) urx = p.X;
            if (p.Y > ury) ury = p.Y;
        }
        const double margin = 5;
        llx -= margin; lly -= margin; urx += margin; ury += margin;

        // Open the PDF, add the polyline annotation, and save
        using (Document doc = new Document(inputPath))
        {
            // For demonstration, add the annotation to the first page
            Page page = doc.Pages[1];

            // Create the annotation with the calculated rectangle and vertices
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, points.ToArray());

            // Optional visual styling
            polyline.Border = new Border(polyline) { Width = 2 };
            polyline.Color = Aspose.Pdf.Color.Blue;                     // stroke color
            polyline.InteriorColor = Aspose.Pdf.Color.LightGray;        // fill color (if needed)

            // Add the annotation to the page
            page.Annotations.Add(polyline);

            // Persist the changes
            doc.Save(outputPath);
        }

        Console.WriteLine($"PolyLine annotation added and saved to '{outputPath}'.");
    }
}
