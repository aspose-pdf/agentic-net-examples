using System;
using System.Drawing; // for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define star points (5‑pointed star) centered at (200,500) with outer radius 100 and inner radius 40
            double centerX = 200;
            double centerY = 500;
            double outerR = 100;
            double innerR = 40;
            double angle = -Math.PI / 2; // start at top

            // Compute the 10 vertices (alternating outer/inner)
            double[] xs = new double[10];
            double[] ys = new double[10];
            for (int i = 0; i < 10; i++)
            {
                double r = (i % 2 == 0) ? outerR : innerR;
                xs[i] = centerX + r * Math.Cos(angle);
                ys[i] = centerY + r * Math.Sin(angle);
                angle += Math.PI / 5; // 36 degrees
            }

            // Build the operator sequence to draw the star
            OperatorCollection ops = page.Contents;

            // Save graphics state (q)
            ops.Add(new GSave());

            // Set fill color (yellow) and stroke color (black)
            // Use float overloads because SetRGBColor does not accept System.Drawing.Color directly
            ops.Add(new SetRGBColor(1f, 1f, 0f));          // non‑stroking (fill) – yellow
            ops.Add(new SetRGBColorStroke(0f, 0f, 0f));    // stroking (outline) – black

            // Set line width for the outline
            ops.Add(new SetLineWidth(2));

            // Begin path at first vertex
            ops.Add(new MoveTo(xs[0], ys[0]));

            // Connect all remaining vertices
            for (int i = 1; i < 10; i++)
            {
                ops.Add(new LineTo(xs[i], ys[i]));
            }

            // Close the path, fill and stroke it
            ops.Add(new ClosePathFillStroke());

            // Restore graphics state (Q)
            ops.Add(new GRestore());

            // Save the resulting PDF
            string outputPath = "star_shape.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"Star shape PDF saved to '{outputPath}'.");
        }
    }
}
