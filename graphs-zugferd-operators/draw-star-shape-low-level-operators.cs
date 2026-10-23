using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "star.pdf";

        // Star parameters
        double centerX = 300;          // X coordinate of star centre
        double centerY = 400;          // Y coordinate of star centre
        double outerRadius = 100;      // Radius of outer points
        double innerRadius = 40;       // Radius of inner points
        int points = 5;                // Number of star points

        // Angle between successive points (outer‑inner)
        double angleStep = Math.PI / points; // half‑step because we alternate outer/inner

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Move to the first outer point (top of the star)
            double startX = centerX + outerRadius * Math.Cos(-Math.PI / 2);
            double startY = centerY + outerRadius * Math.Sin(-Math.PI / 2);
            page.Contents.Add(new MoveTo(startX, startY));

            // Draw the star by alternating outer and inner vertices
            for (int i = 1; i < points * 2; i++)
            {
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angle = -Math.PI / 2 + i * angleStep;
                double x = centerX + radius * Math.Cos(angle);
                double y = centerY + radius * Math.Sin(angle);
                page.Contents.Add(new LineTo(x, y));
            }

            // Close the path to complete the shape
            page.Contents.Add(new ClosePath());

            // Set a fill color (gold) and fill the star
            // Aspose.Pdf does not provide a SetFillColor operator; use SetRGBColor which maps to the PDF "rg" operator (fill color).
            page.Contents.Add(new SetRGBColor(1.0f, 0.84f, 0.0f)); // gold fill
            page.Contents.Add(new Fill());

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Star PDF saved to '{outputPath}'.");
    }
}
