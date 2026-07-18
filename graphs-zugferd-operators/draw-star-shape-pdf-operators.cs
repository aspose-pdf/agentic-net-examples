using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class StarDrawer
{
    static void Main()
    {
        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Set stroke color to red (RGB: 1,0,0)
            page.Contents.Add(new SetRGBColorStroke(1, 0, 0));
            // Set fill color to yellow (RGB: 1,1,0)
            page.Contents.Add(new SetRGBColor(1, 1, 0));
            // Set line width for the star outline
            page.Contents.Add(new SetLineWidth(2));

            // Parameters for the star geometry
            double centerX = 300;
            double centerY = 500;
            double outerRadius = 100;
            double innerRadius = 40;
            int pointCount = 5; // 5‑pointed star

            // Compute the 10 vertices (alternating outer/inner points)
            var vertices = new List<(double X, double Y)>();
            for (int i = 0; i < pointCount * 2; i++)
            {
                double angleDeg = -90 + i * 36; // start at -90° (upwards)
                double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                double angleRad = angleDeg * Math.PI / 180.0;
                double x = centerX + radius * Math.Cos(angleRad);
                double y = centerY + radius * Math.Sin(angleRad);
                vertices.Add((x, y));
            }

            // Begin the path at the first vertex
            page.Contents.Add(new MoveTo(vertices[0].X, vertices[0].Y));

            // Draw lines to the remaining vertices
            for (int i = 1; i < vertices.Count; i++)
            {
                page.Contents.Add(new LineTo(vertices[i].X, vertices[i].Y));
            }

            // Close the path and fill+stroke the star
            page.Contents.Add(new ClosePathFillStroke());

            // Save the resulting PDF
            doc.Save("star.pdf");
        }
    }
}