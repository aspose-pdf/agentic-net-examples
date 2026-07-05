using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ReportGenerator
{
    static void Main()
    {
        const string outputPath = "Report.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define graph dimensions (graph area for the bars)
            double graphWidth = 500;
            double graphHeight = 200;
            double graphLeft = 50;
            double graphTop = 500; // Y coordinate from bottom of the page

            // Create the Graph container for the bar chart
            var graph = new Aspose.Pdf.Drawing.Graph((float)graphWidth, (float)graphHeight)
            {
                Left = (float)graphLeft,
                Top = (float)graphTop
            };

            // Sample data categories (label, color, value)
            var categories = new[]
            {
                new { Label = "Category A", Color = Aspose.Pdf.Color.FromArgb(255, 0, 0), Value = 120.0 },
                new { Label = "Category B", Color = Aspose.Pdf.Color.FromArgb(0, 128, 0), Value = 80.0 },
                new { Label = "Category C", Color = Aspose.Pdf.Color.FromArgb(0, 0, 255), Value = 150.0 }
            };

            // Determine scaling factor to fit values within graph height
            double maxValue = 0;
            foreach (var c in categories)
                if (c.Value > maxValue) maxValue = c.Value;
            double heightScale = graphHeight / maxValue;

            // Draw rectangles (bars) for each category
            double rectWidth = graphWidth / categories.Length - 10; // spacing of 10 points
            double currentX = 5; // start with a small left margin inside the graph

            foreach (var cat in categories)
            {
                double rectHeight = cat.Value * heightScale;
                // Use Aspose.Pdf.Drawing.Rectangle for graph shapes
                var shapeRect = new Aspose.Pdf.Drawing.Rectangle((float)currentX, 0f, (float)rectWidth, (float)rectHeight)
                {
                    GraphInfo = new GraphInfo
                    {
                        FillColor = cat.Color,
                        Color = cat.Color, // border color
                        LineWidth = 1f
                    }
                };
                graph.Shapes.Add(shapeRect);
                currentX += rectWidth + 10;
            }

            // Add the bar‑graph to the page
            page.Paragraphs.Add(graph);

            // ----- Legend -----
            // Position legend to the right of the graph
            double legendLeft = graphLeft + graphWidth + 30;
            double legendTop = graphTop + graphHeight; // start from top of graph
            double legendItemHeight = 20;
            double legendSquareSize = 12;
            double legendSpacing = 5;

            // Create a separate Graph that will hold the legend squares (so they are drawn as shapes)
            var legendGraph = new Aspose.Pdf.Drawing.Graph(200f, (float)(categories.Length * (legendItemHeight + legendSpacing)))
            {
                Left = (float)legendLeft,
                Top = (float)legendTop
            };

            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];
                double itemY = (categories.Length - 1 - i) * (legendItemHeight + legendSpacing) + legendSquareSize;

                // Colored square for legend (inside the legend graph)
                var legendSquare = new Aspose.Pdf.Drawing.Rectangle(0f, (float)itemY - (float)legendSquareSize, (float)legendSquareSize, (float)legendSquareSize)
                {
                    GraphInfo = new GraphInfo
                    {
                        FillColor = cat.Color,
                        Color = cat.Color,
                        LineWidth = 1f
                    }
                };
                legendGraph.Shapes.Add(legendSquare);

                // Text label next to the square – positioned on the page (not inside the graph)
                var tf = new TextFragment(cat.Label)
                {
                    Position = new Position((float)(legendLeft + legendSquareSize + 5), (float)(legendTop - i * (legendItemHeight + legendSpacing) - legendSquareSize / 2)),
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };
                page.Paragraphs.Add(tf);
            }

            // Add the legend graph to the page
            page.Paragraphs.Add(legendGraph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF report generated: {System.IO.Path.GetFullPath(outputPath)}");
    }
}
