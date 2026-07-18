using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "barchart.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold the rectangle shapes
            // Width and height define the drawing area for the graph
            Graph graph = new Graph(600, 200);

            // Parameters for the bar chart
            double barWidth = 30;               // Width of each bar
            double barHeight = 20;              // Height of each bar
            double startX = 50;                 // X coordinate of the first bar
            double startY = 100;                // Y coordinate (bottom) for all bars
            double xIncrement = 40;             // Horizontal distance between bars

            // Loop to add ten rectangles (bars) with incremental X positions
            for (int i = 0; i < 10; i++)
            {
                // Calculate the X position for the current bar
                double x = startX + i * xIncrement;

                // Create a rectangle shape (left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    (float)x,               // left (X)
                    (float)startY,          // bottom (Y)
                    (float)barWidth,        // width
                    (float)barHeight);      // height

                // Set visual properties via GraphInfo (FillColor, Border Color, LineWidth)
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };

                // Add the rectangle to the graph's shape collection
                graph.Shapes.Add(rect);
            }

            // Add the graph (containing all rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bar chart PDF saved to '{outputPath}'.");
    }
}