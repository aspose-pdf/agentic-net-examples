using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a graph container (width: 400pt, height: 300pt)
            Graph graph = new Graph(400, 300);

            // ----- Rectangle shape -----
            // Parameters: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 150, 200, 100);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,   // Fill color
                Color = Color.DarkBlue,        // Stroke color
                LineWidth = 2                  // Stroke width
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height
            Ellipse ellipseShape = new Ellipse(300, 150, 150, 100);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGreen,
                Color = Color.DarkGreen,
                LineWidth = 2
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}