using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class PieChartExample
{
    static void Main()
    {
        // Input data for the pie chart (sales distribution)
        double[] values = { 40, 30, 20, 10 }; // percentages or any relative numbers
        // Distinct colors for each slice (RGB components in range 0..1)
        double[][] sliceRgb = new double[][]
        {
            new double[] { 0.9, 0.1, 0.1 }, // red
            new double[] { 0.1, 0.9, 0.1 }, // green
            new double[] { 0.1, 0.1, 0.9 }, // blue
            new double[] { 0.9, 0.9, 0.1 }  // yellow
        };

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define pie chart geometry
            double centerX = 300; // X coordinate of the center
            double centerY = 400; // Y coordinate of the center
            double radius = 150; // radius of the pie

            // Compute total to calculate angles
            double total = 0;
            foreach (double v in values) total += v;

            double startAngle = 0; // in degrees

            for (int i = 0; i < values.Length; i++)
            {
                double sweepAngle = values[i] / total * 360.0; // slice angle in degrees
                double endAngle = startAngle + sweepAngle;

                // Convert angles to radians for trigonometric functions
                double startRad = startAngle * Math.PI / 180.0;
                double endRad = endAngle * Math.PI / 180.0;

                // Calculate points on the circumference
                double startX = centerX + radius * Math.Cos(startRad);
                double startY = centerY + radius * Math.Sin(startRad);
                double endX = centerX + radius * Math.Cos(endRad);
                double endY = centerY + radius * Math.Sin(endRad);

                // Set fill color for the slice (non‑stroking color)
                page.Contents.Add(new SetRGBColor(
                    sliceRgb[i][0],
                    sliceRgb[i][1],
                    sliceRgb[i][2]));

                // Begin path: move to center, line to start point, line to end point, close path
                page.Contents.Add(new MoveTo(centerX, centerY));
                page.Contents.Add(new LineTo(startX, startY));
                page.Contents.Add(new LineTo(endX, endY));
                page.Contents.Add(new ClosePath());

                // Fill the sector
                page.Contents.Add(new Fill());

                // Optional: draw a thin border around the slice
                page.Contents.Add(new SetRGBColorStroke(0, 0, 0)); // black stroke
                page.Contents.Add(new SetLineWidth(0.5));
                page.Contents.Add(new Stroke());

                // Prepare for next slice
                startAngle = endAngle;
            }

            // Save the resulting PDF
            doc.Save("PieChart.pdf");
        }

        Console.WriteLine("Pie chart PDF created successfully.");
    }
}
