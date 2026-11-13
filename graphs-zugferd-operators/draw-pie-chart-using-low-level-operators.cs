using System;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string outputPath = "piechart.pdf";

        // Sample sales data (percentages)
        double[] values = { 30, 20, 25, 15, 10 };
        double total = 0;
        foreach (double v in values) total += v;

        // Page size and pie geometry
        double pageWidth = 500;
        double pageHeight = 500;
        double centerX = pageWidth / 2;
        double centerY = pageHeight / 2;
        double radius = 200;

        // Distinct slice colors (RGB components in 0‑1 range)
        double[][] sliceColors = new double[][]
        {
            new double[] { 0.9, 0.1, 0.1 }, // red
            new double[] { 0.1, 0.9, 0.1 }, // green
            new double[] { 0.1, 0.1, 0.9 }, // blue
            new double[] { 0.9, 0.9, 0.1 }, // yellow
            new double[] { 0.9, 0.1, 0.9 }  // magenta
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page with defined dimensions
            Page page = doc.Pages.Add();
            page.PageInfo.Width = pageWidth;
            page.PageInfo.Height = pageHeight;

            double startAngle = 0;

            for (int i = 0; i < values.Length; i++)
            {
                // Compute sweep angle for current slice
                double sweep = values[i] / total * 360.0;
                double endAngle = startAngle + sweep;

                // Convert angles to radians
                double radStart = startAngle * Math.PI / 180.0;
                double radEnd   = endAngle   * Math.PI / 180.0;

                // Points on the circumference for the slice edges
                double x1 = centerX + radius * Math.Cos(radStart);
                double y1 = centerY + radius * Math.Sin(radStart);
                double x2 = centerX + radius * Math.Cos(radEnd);
                double y2 = centerY + radius * Math.Sin(radEnd);

                // Begin a new graphics state for the slice
                page.Contents.Add(new GSave());

                // Set fill colour for the slice
                page.Contents.Add(new SetRGBColor(
                    sliceColors[i][0],
                    sliceColors[i][1],
                    sliceColors[i][2]));

                // Build the triangular sector: centre → point1 → point2 → close → fill
                page.Contents.Add(new MoveTo(centerX, centerY));
                page.Contents.Add(new LineTo(x1, y1));
                page.Contents.Add(new LineTo(x2, y2));
                page.Contents.Add(new ClosePath());
                page.Contents.Add(new Fill());

                // Restore the previous graphics state
                page.Contents.Add(new GRestore());

                startAngle = endAngle; // next slice starts where this one ended
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pie chart saved to '{outputPath}'.");
    }
}
