using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "star_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Draw on the first page
            Page page = doc.Pages[1];
            OperatorCollection oc = page.Contents;

            // Star parameters
            double cx = 300;      // center X
            double cy = 400;      // center Y
            double outer = 100;   // outer radius
            double inner = 50;    // inner radius
            double startAngle = -Math.PI / 2; // start at top point

            // Compute the 10 points of a 5‑point star
            List<Aspose.Pdf.Point> pts = new List<Aspose.Pdf.Point>();
            double angle = startAngle;
            for (int i = 0; i < 5; i++)
            {
                // outer vertex
                double ox = cx + outer * Math.Cos(angle);
                double oy = cy + outer * Math.Sin(angle);
                pts.Add(new Aspose.Pdf.Point(ox, oy));
                angle += Math.PI / 5; // 36°

                // inner vertex
                double ix = cx + inner * Math.Cos(angle);
                double iy = cy + inner * Math.Sin(angle);
                pts.Add(new Aspose.Pdf.Point(ix, iy));
                angle += Math.PI / 5; // 36°
            }

            // Build the operator sequence for the star
            List<Operator> starOps = new List<Operator>();

            // Save graphics state (GSave corresponds to the low‑level 'q' operator)
            starOps.Add(new GSave());

            // Set fill color (light yellow) – rg operator
            starOps.Add(new SetRGBColor(1.0, 1.0, 0.0));

            // Set stroke color (orange) – RG operator
            starOps.Add(new SetRGBColorStroke(1.0, 0.5, 0.0));

            // Set line width
            starOps.Add(new SetLineWidth(2));

            // Begin path at first point
            starOps.Add(new MoveTo(pts[0].X, pts[0].Y));

            // Add line segments to remaining points
            for (int i = 1; i < pts.Count; i++)
            {
                starOps.Add(new LineTo(pts[i].X, pts[i].Y));
            }

            // Close the path and fill+stroke it
            starOps.Add(new ClosePathFillStroke());

            // Restore graphics state (GRestore corresponds to the low‑level 'Q' operator)
            starOps.Add(new GRestore());

            // Append the operators to the page content
            oc.Add(starOps.ToArray());

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star shape added and saved to '{outputPath}'.");
    }
}
