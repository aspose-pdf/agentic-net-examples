using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class PieChartExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Center and radius of the pie chart
            double cx = 300;   // X coordinate of center
            double cy = 400;   // Y coordinate of center
            double radius = 150;

            // Sales distribution (must sum to 1.0)
            double[] values = { 0.4, 0.3, 0.2, 0.1 };

            // Distinct colors for each slice (RGB components in 0..1 range)
            double[][] rgbColors = new double[][]
            {
                new double[] { 1.0, 0.0, 0.0 }, // Red
                new double[] { 0.0, 1.0, 0.0 }, // Green
                new double[] { 0.0, 0.0, 1.0 }, // Blue
                new double[] { 1.0, 1.0, 0.0 }  // Yellow
            };

            double startAngle = 0.0; // Starting angle in degrees

            for (int i = 0; i < values.Length; i++)
            {
                double sweep = values[i] * 360.0;          // Angle covered by this slice
                double endAngle = startAngle + sweep;      // End angle for this slice

                // Set the fill color for the current slice
                page.Contents.Add(new SetRGBColor(
                    rgbColors[i][0],
                    rgbColors[i][1],
                    rgbColors[i][2]));

                // Begin a new path at the center of the pie
                page.Contents.Add(new MoveTo(cx, cy));

                // First point on the outer arc
                double startRad = startAngle * Math.PI / 180.0;
                double xStart = cx + radius * Math.Cos(startRad);
                double yStart = cy + radius * Math.Sin(startRad);
                page.Contents.Add(new LineTo(xStart, yStart));

                // Approximate the arc with short line segments (5° steps)
                int steps = (int)Math.Ceiling(sweep / 5.0);
                for (int s = 1; s <= steps; s++)
                {
                    double angle = startAngle + (s * sweep / steps);
                    double rad = angle * Math.PI / 180.0;
                    double x = cx + radius * Math.Cos(rad);
                    double y = cy + radius * Math.Sin(rad);
                    page.Contents.Add(new LineTo(x, y));
                }

                // Close the path back to the center and fill it
                page.Contents.Add(new ClosePath());
                page.Contents.Add(new Fill());

                // Prepare for the next slice
                startAngle = endAngle;
            }

            // Save the resulting PDF
            doc.Save("pie_chart.pdf");
        }
    }
}
