using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

public class DrawPieChartOperators
{
    public static void Main()
    {
        // Create a sample PDF to work with (self‑contained example)
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // Open the sample PDF and draw the pie chart using low‑level operators
        using (Document doc = new Document("input.pdf"))
        {
            Page page = doc.Pages[1];

            double centerX = 300.0;
            double centerY = 400.0;
            double radius = 150.0;

            double[] values = new double[] { 40.0, 30.0, 20.0, 10.0 };
            double[][] colors = new double[][] {
                new double[] { 1.0, 0.0, 0.0 }, // red
                new double[] { 0.0, 1.0, 0.0 }, // green
                new double[] { 0.0, 0.0, 1.0 }, // blue
                new double[] { 1.0, 1.0, 0.0 }  // yellow
            };

            double startAngle = 0.0;
            for (int i = 0; i < values.Length && i < 4; i++)
            {
                double sweepAngle = 360.0 * values[i] / 100.0;

                // Set fill color for the slice
                page.Contents.Add(new SetRGBColor(colors[i][0], colors[i][1], colors[i][2]));

                // Move to the centre of the pie
                page.Contents.Add(new MoveTo(centerX, centerY));

                // Draw line from centre to the start point on the circumference
                double startRad = startAngle * Math.PI / 180.0;
                double xStart = centerX + radius * Math.Cos(startRad);
                double yStart = centerY + radius * Math.Sin(startRad);
                page.Contents.Add(new LineTo(xStart, yStart));

                // Draw the arc using one or two cubic Bezier curves (CurveTo operator)
                AddArcWithCurveTo(page, centerX, centerY, radius, startAngle, sweepAngle);

                // Close the slice back to the centre and fill it
                page.Contents.Add(new LineTo(centerX, centerY));
                page.Contents.Add(new Fill());

                startAngle += sweepAngle;
            }

            doc.Save("output.pdf");
        }
    }

    // Helper method that approximates a circular arc with one or two CurveTo operators
    private static void AddArcWithCurveTo(Page page, double cx, double cy, double r, double startDeg, double sweepDeg)
    {
        // Normalise sweep to the range (0, 360]
        double remainingSweep = sweepDeg;
        double currentStart = startDeg;

        while (remainingSweep > 0)
        {
            // Use a maximum of 180 degrees per Bezier segment (as the approximation is accurate for <= 180°)
            double segmentSweep = remainingSweep > 180.0 ? 180.0 : remainingSweep;

            double startRad = currentStart * Math.PI / 180.0;
            double endRad = (currentStart + segmentSweep) * Math.PI / 180.0;
            double delta = segmentSweep * Math.PI / 180.0;

            // Compute end point of the segment
            double xEnd = cx + r * Math.Cos(endRad);
            double yEnd = cy + r * Math.Sin(endRad);

            // Compute the control points using the standard approximation for a circular arc
            double k = 4.0 / 3.0 * Math.Tan(delta / 4.0);
            double xCtrl1 = cx + r * (Math.Cos(startRad) - k * Math.Sin(startRad));
            double yCtrl1 = cy + r * (Math.Sin(startRad) + k * Math.Cos(startRad));
            double xCtrl2 = cx + r * (Math.Cos(endRad) + k * Math.Sin(endRad));
            double yCtrl2 = cy + r * (Math.Sin(endRad) - k * Math.Cos(endRad));

            // Add the CurveTo operator (control point 1, control point 2, end point)
            page.Contents.Add(new CurveTo(xCtrl1, yCtrl1, xCtrl2, yCtrl2, xEnd, yEnd));

            // Prepare for the next segment (if any)
            currentStart += segmentSweep;
            remainingSweep -= segmentSweep;
        }
    }
}