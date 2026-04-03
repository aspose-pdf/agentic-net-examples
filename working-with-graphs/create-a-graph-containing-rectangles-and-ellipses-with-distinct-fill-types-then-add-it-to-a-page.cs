using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a graph container with a specific size (width, height in points)
            Graph graph = new Graph(400, 300);

            // ----- Aspose.Pdf.Rectangle shape -----
            // Parameters: left, bottom, width, height
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 150, 200, 100);
            // Set visual properties via GraphInfo (FillColor, Stroke Color, LineWidth)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color = Color.DarkBlue,
                LineWidth = 2
            };
            // Add rectangle to the graph
            graph.Shapes.Add(rect);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height
            Ellipse ellipse = new Ellipse(300, 150, 150, 100);
            // Set visual properties via GraphInfo
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color = Color.Red,
                LineWidth = 1.5f
            };
            // Add ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the completed graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}