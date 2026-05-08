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
            // Add a single page
            Page page = doc.Pages.Add();

            // Define pie chart parameters
            double centerX = 300;   // X coordinate of the center
            double centerY = 400;   // Y coordinate of the center
            double radius  = 150;   // Radius of the pie

            // Sample sales distribution (percentages that sum to 100)
            double[] sales = { 30, 20, 25, 25 };

            // Distinct colors for each slice (RGB values in 0..1 range)
            double[][] colors = new double[][]
            {
                new double[] { 1.0, 0.0, 0.0 }, // Red
                new double[] { 0.0, 1.0, 0.0 }, // Green
                new double[] { 0.0, 0.0, 1.0 }, // Blue
                new double[] { 1.0, 1.0, 0.0 }  // Yellow
            };

            double startAngle = 0.0; // In degrees

            for (int i = 0; i < sales.Length; i++)
            {
                // Compute sweep angle for the current slice
                double sweepAngle = sales[i] / 100.0 * 360.0;
                double endAngle = startAngle + sweepAngle;

                // Convert angles to radians for trigonometric functions
                double startRad = startAngle * Math.PI / 180.0;
                double endRad   = endAngle   * Math.PI / 180.0;

                // Compute points on the circumference
                double x1 = centerX + radius * Math.Cos(startRad);
                double y1 = centerY + radius * Math.Sin(startRad);
                double x2 = centerX + radius * Math.Cos(endRad);
                double y2 = centerY + radius * Math.Sin(endRad);

                // Begin a new path for the slice
                page.Contents.Add(new MoveTo(centerX, centerY));
                page.Contents.Add(new LineTo(x1, y1));
                page.Contents.Add(new LineTo(x2, y2));
                page.Contents.Add(new ClosePath());

                // Set fill color for the slice
                double[] col = colors[i % colors.Length];
                page.Contents.Add(new SetColor(col[0], col[1], col[2]));

                // Fill the slice using non‑zero winding rule
                page.Contents.Add(new Fill());

                // Update start angle for the next slice
                startAngle = endAngle;
            }

            // Save the PDF (extension is ignored, output is always PDF)
            doc.Save("piechart.pdf");
        }
    }
}