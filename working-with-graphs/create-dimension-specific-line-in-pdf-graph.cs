using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "line_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts like a drawing canvas) with a specific size
            Graph graph = new Graph(400, 200); // width = 400pt, height = 200pt
            graph.Left = 50;   // position from the left edge of the page
            graph.Top  = 600;  // position from the bottom edge of the page

            // Define a line: start point (0,0) to end point (300,0) within the graph's coordinate system
            float[] lineCoordinates = { 0f, 0f, 300f, 0f };
            Line line = new Line(lineCoordinates);

            // Set visual properties of the line via GraphInfo (color and thickness)
            line.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Blue,
                LineWidth = 2f
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Add the graph (which now contains the line) to the page's content
            page.Paragraphs.Add(graph);

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dimension‑specific line saved to '{outputPath}'.");
    }
}