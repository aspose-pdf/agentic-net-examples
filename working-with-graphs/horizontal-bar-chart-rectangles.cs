using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class BarChartExample
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "BarChart.pdf";

        // Ensure the output directory exists
        string outputDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Parameters for the bar chart
            double startX = 50;          // X position of the first bar
            double barWidth = 30;        // Width of each bar
            double barSpacing = 10;      // Space between bars
            double baseY = 100;          // Y position of the bar base
            double maxBarHeight = 200;   // Maximum height of a bar

            // Loop to create ten bars
            for (int i = 0; i < 10; i++)
            {
                // Compute X position for the current bar
                double currentX = startX + i * (barWidth + barSpacing);

                // Example: bar height varies (here simply i * 15 + 20)
                double barHeight = 20 + i * 15;
                if (barHeight > maxBarHeight) barHeight = maxBarHeight;

                // Create a Graph container (size large enough to hold the rectangle)
                // Use the double‑based constructor as recommended.
                Graph graph = new Graph(500.0, 400.0);

                // Create a rectangle shape (left, bottom, width, height)
                // Use the drawing rectangle (Aspose.Pdf.Drawing.Rectangle) and float parameters.
                Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)currentX,
                    (float)baseY,
                    (float)barWidth,
                    (float)barHeight);

                // Set visual properties via GraphInfo
                shape.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightBlue,
                    Color = Aspose.Pdf.Color.DarkBlue,
                    LineWidth = 1f
                };

                // Add the rectangle to the Graph's shape collection
                graph.Shapes.Add(shape);

                // Add the Graph (containing the rectangle) to the page
                page.Paragraphs.Add(graph);
            }

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
    }
}
