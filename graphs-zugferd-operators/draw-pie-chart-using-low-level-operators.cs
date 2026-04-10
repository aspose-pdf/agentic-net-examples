using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class PieChartExample
{
    static void Main()
    {
        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define pie chart parameters
            double centerX = 300;   // X coordinate of the center
            double centerY = 400;   // Y coordinate of the center
            double radius  = 150;   // Radius of the pie

            // Sample sales distribution (percentages)
            double[] sales = { 40, 30, 30 }; // must sum to 100
            // Corresponding slice colors (R,G,B) values in 0..1 range
            double[,] colors = {
                { 0.9, 0.1, 0.1 }, // red
                { 0.1, 0.9, 0.1 }, // green
                { 0.1, 0.1, 0.9 }  // blue
            };

            double startAngle = 0; // in degrees

            for (int i = 0; i < sales.Length; i++)
            {
                // Compute sweep angle for the current slice
                double sweepAngle = sales[i] * 360.0 / 100.0;
                double endAngle = startAngle + sweepAngle;

                // Convert angles to radians for trigonometric functions
                double startRad = startAngle * Math.PI / 180.0;
                double endRad   = endAngle   * Math.PI / 180.0;

                // Compute points on the circumference for start and end angles
                double x1 = centerX + radius * Math.Cos(startRad);
                double y1 = centerY + radius * Math.Sin(startRad);
                double x2 = centerX + radius * Math.Cos(endRad);
                double y2 = centerY + radius * Math.Sin(endRad);

                // Set fill color for the slice (non‑stroking color)
                page.Contents.Add(new SetRGBColor(
                    colors[i, 0], // red component
                    colors[i, 1], // green component
                    colors[i, 2]  // blue component
                ));

                // Build the slice path: center -> start point -> end point -> close -> fill
                page.Contents.Add(new MoveTo(centerX, centerY));
                page.Contents.Add(new LineTo(x1, y1));
                page.Contents.Add(new LineTo(x2, y2));
                page.Contents.Add(new ClosePath());
                page.Contents.Add(new Fill());

                // Advance start angle for the next slice
                startAngle = endAngle;
            }

            // Save the PDF
            doc.Save("piechart.pdf");
        }
    }
}