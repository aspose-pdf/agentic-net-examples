using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the size of the graph (width x height)
            double graphWidth = 400;
            double graphHeight = 300;

            // Create the Graph object and position it on the page
            Graph graph = new Graph(graphWidth, graphHeight)
            {
                Left = 50,   // distance from the left edge of the page
                Top  = 400   // distance from the bottom edge of the page
            };

            // Sample data for the bar chart
            double[] values = { 120, 80, 150, 60 };
            Aspose.Pdf.Color[] barColors = {
                Aspose.Pdf.Color.Chocolate,
                Aspose.Pdf.Color.Chartreuse,
                Aspose.Pdf.Color.Blue,
                Aspose.Pdf.Color.Red
            };

            // Determine bar width, spacing and scaling factor
            double barWidth = graphWidth / values.Length * 0.6;
            double spacing  = (graphWidth - barWidth * values.Length) / (values.Length + 1);
            double maxValue = 200; // maximum value for Y‑axis scaling

            // Draw each bar as a rectangle shape inside the graph
            for (int i = 0; i < values.Length; i++)
            {
                double barHeight = (values[i] / maxValue) * graphHeight;
                double x = spacing + i * (barWidth + spacing);
                double y = graphHeight - barHeight; // origin is bottom of the graph

                // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
                var bar = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,
                    (float)y,
                    (float)barWidth,
                    (float)barHeight);

                bar.GraphInfo = new GraphInfo
                {
                    FillColor = barColors[i],          // custom fill color
                    Color     = Aspose.Pdf.Color.Black, // border color
                    LineWidth = 1f
                };

                graph.Shapes.Add(bar);
            }

            // Add X‑axis label below the graph
            TextFragment xLabel = new TextFragment("Categories");
            xLabel.Position = new Position(200, 380); // adjust as needed
            xLabel.TextState.FontSize = 12;
            xLabel.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(xLabel);

            // Add Y‑axis label to the left of the graph (rotated 90°)
            TextFragment yLabel = new TextFragment("Values");
            yLabel.Position = new Position(20, 550); // adjust as needed
            yLabel.TextState.FontSize = 12;
            yLabel.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            yLabel.TextState.Rotation = 90;
            page.Paragraphs.Add(yLabel);

            // Add the completed graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart saved to '{outputPath}'.");
    }
}
