using System;
using System.IO;
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
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a graph with the desired size (width, height in points)
            Graph graph = new Graph(400, 200);

            // Configure visual appearance via GraphInfo
            graph.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // background fill
                Color = Color.Blue,            // outline color
                LineWidth = 2                  // outline thickness
            };

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to '{outputPath}'.");
    }
}