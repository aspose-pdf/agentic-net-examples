using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "horizontal_bar_chart.pdf";

        // Ensure the output directory exists (fully qualify System.IO.Path to avoid ambiguity)
        string outputDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the bar chart
            Page page = doc.Pages.Add();

            // Define common rectangle height and vertical position
            double rectHeight = 30;
            double rectY = 500; // Y coordinate of the lower edge

            // Create a Graph that covers the whole page – Graph is the only paragraph that can contain drawing shapes
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Loop to create ten rectangles with incremental X coordinates
            for (int i = 0; i < 10; i++)
            {
                // Incremental X coordinate (spacing of 40 units between bars)
                double rectX = 50 + i * 40;

                // Drawing.Rectangle constructor: left, bottom, width, height
                Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(
                    (float)rectX,
                    (float)rectY,
                    30f,
                    (float)rectHeight);

                // Styling via GraphInfo – FillColor for the interior, Color for the border
                shape.GraphInfo.FillColor = Color.LightGray;
                shape.GraphInfo.Color = Color.Black;
                shape.GraphInfo.LineWidth = 0.5f;

                // Add the shape to the graph
                graph.Shapes.Add(shape);
            }

            // Add the graph (which now contains all rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
    }
}
