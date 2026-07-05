using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class PieChartExample
{
    static void Main()
    {
        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Center of the pie chart and radius
            double cx = 300; // X coordinate of center
            double cy = 400; // Y coordinate of center
            double radius = 150;

            // Sample data values (must sum to 100 for percentage calculation)
            double[] values = { 30, 20, 50 };
            // Corresponding colors for each slice (R,G,B values in range 0..1)
            double[][] colors = {
                new double[] { 1.0, 0.0, 0.0 }, // Red
                new double[] { 0.0, 1.0, 0.0 }, // Green
                new double[] { 0.0, 0.0, 1.0 }  // Blue
            };

            double total = 0;
            foreach (double v in values) total += v;

            double startAngle = 0.0; // in degrees

            for (int i = 0; i < values.Length; i++)
            {
                double sweepAngle = 360.0 * values[i] / total;
                double endAngle = startAngle + sweepAngle;

                // Convert angles to radians for trigonometric functions
                double startRad = startAngle * Math.PI / 180.0;
                double endRad   = endAngle   * Math.PI / 180.0;

                // Points on the circumference for start and end of the slice
                double x1 = cx + radius * Math.Cos(startRad);
                double y1 = cy + radius * Math.Sin(startRad);
                double x2 = cx + radius * Math.Cos(endRad);
                double y2 = cy + radius * Math.Sin(endRad);

                // Set fill color for this slice (must be before the Fill operator)
                double[] col = colors[i];
                page.Contents.Add(new SetRGBColor(col[0], col[1], col[2]));

                // Begin a new path: move to center, line to start point, line to end point, close path
                page.Contents.Add(new MoveTo(cx, cy));
                page.Contents.Add(new LineTo(x1, y1));
                page.Contents.Add(new LineTo(x2, y2));
                page.Contents.Add(new ClosePath());

                // Fill the slice (non‑zero winding rule)
                page.Contents.Add(new Fill());

                // Prepare for next slice
                startAngle = endAngle;
            }

            // Save the PDF
            doc.Save("pie_chart.pdf");
        }
    }
}
