using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "boundscheck_graph.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a graph (container) with a defined width and height (double literals as required)
            Graph graph = new Graph(400.0, 200.0); // width = 400 points, height = 200 points
            graph.Left = 50;   // position from the left edge of the page
            graph.Top = 600;   // position from the bottom edge of the page

            // Create a rectangle shape to place inside the graph – fully qualified to avoid ambiguity
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,   // left
                0f,   // bottom
                100f, // width
                50f   // height
            );
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the shape to the graph's Shapes collection
            graph.Shapes.Add(rect);

            // Enable bounds checking: throw an exception if any shape does not fit within the graph
            // Supply the container dimensions as double values
            graph.Shapes.UpdateBoundsCheckMode(
                BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                graph.Width,
                graph.Height);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
