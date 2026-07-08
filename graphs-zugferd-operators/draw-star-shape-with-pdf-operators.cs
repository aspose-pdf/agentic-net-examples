using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        // Output PDF file
        const string outputPath = "star_output.pdf";

        // Star geometry parameters
        const double centerX = 200;        // X coordinate of star centre
        const double centerY = 500;        // Y coordinate of star centre
        const double outerRadius = 100;    // Radius of outer vertices
        const double innerRadius = 50;     // Radius of inner vertices
        const int points = 5;              // Number of star points (5‑pointed star)

        // Create a new PDF document with a single blank page (self‑contained example)
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add(); // default A4 size

            // Get the low‑level operator collection of the page's content stream
            OperatorCollection ops = page.Contents;

            // Save graphics state (q)
            ops.Add(new GSave());

            // Set stroke color (red) and fill color (yellow)
            ops.Add(new SetRGBColorStroke(1.0, 0.0, 0.0)); // red stroke
            ops.Add(new SetRGBColor(1.0, 1.0, 0.0));      // yellow fill

            // Set line width for the star outline
            ops.Add(new SetLineWidth(2));

            // Calculate the first outer vertex (starting at the top of the star)
            double angle = -Math.PI / 2; // -90° so the star points upward
            double step = Math.PI / points; // angle step between vertices

            double x0 = centerX + outerRadius * Math.Cos(angle);
            double y0 = centerY + outerRadius * Math.Sin(angle);
            ops.Add(new MoveTo(x0, y0));

            // Alternate between outer and inner vertices to create the star shape
            for (int i = 1; i < points * 2; i++)
            {
                angle += step;
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                ops.Add(new LineTo(x, y));
            }

            // Close the path and fill+stroke it
            ops.Add(new ClosePathFillStroke());

            // Restore graphics state (Q)
            ops.Add(new GRestore());

            // Save the modified PDF
            doc.Save(outputPath);
        }
    }
}
