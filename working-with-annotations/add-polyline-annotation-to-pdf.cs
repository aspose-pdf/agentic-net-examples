using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "polyline_annotation.pdf";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define a list of points for the polyline (Aspose.Pdf.Point)
            List<Point> points = new List<Point>
            {
                new Point(100, 500),
                new Point(200, 400),
                new Point(300, 600),
                new Point(400, 450)
            };

            // Compute a simple bounding rectangle that contains all points
            double llx = double.MaxValue, lly = double.MaxValue;
            double urx = double.MinValue, ury = double.MinValue;
            foreach (Point p in points)
            {
                if (p.X < llx) llx = p.X;
                if (p.Y < lly) lly = p.Y;
                if (p.X > urx) urx = p.X;
                if (p.Y > ury) ury = p.Y;
            }
            // Add a small margin around the polyline
            const double margin = 10;
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                llx - margin, lly - margin, urx + margin, ury + margin);

            // Create the PolylineAnnotation first, then set its visual properties.
            PolylineAnnotation polyline = new PolylineAnnotation(page, rect, points.ToArray());
            polyline.Color = Aspose.Pdf.Color.Blue;
            polyline.Opacity = 0.8f;
            // Border must be created after the annotation instance exists.
            polyline.Border = new Border(polyline) { Width = 2 };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(polyline);

            // Save the PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with polyline annotation saved to '{outputPath}'.");
    }
}
