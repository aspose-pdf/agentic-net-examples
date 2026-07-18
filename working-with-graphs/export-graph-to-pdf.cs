using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new empty PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values
            Graph graph = new Graph(400.0, 200.0);

            // Create a drawing rectangle (the shape) with float parameters
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f
            };
            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to '{outputPath}'.");
    }
}