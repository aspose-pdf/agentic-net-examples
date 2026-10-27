using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph (vector drawing) using page dimensions
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create a line using the constructor that accepts a float array
            // Coordinates are expressed in the graph's coordinate system (origin at bottom‑left)
            Line line = new Line(new float[] { 50, 750, 550, 750 });
            line.GraphInfo.Color = Color.Black;
            line.GraphInfo.LineWidth = 2;

            // Add the line to the graph's Shapes collection
            graph.Shapes.Add(line);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to PDF: {outputPath}");
    }
}
