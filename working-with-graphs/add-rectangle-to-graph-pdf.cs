using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container with a specific size (width x height in points)
            Graph graph = new Graph(400, 200);

            // Define a rectangle with exact dimensions:
            // left = 50, bottom = 50, width = 200, height = 100
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);

            // Set visual styling via GraphInfo (fill color, border color, line width)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 2
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph (which now contains the rectangle) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}