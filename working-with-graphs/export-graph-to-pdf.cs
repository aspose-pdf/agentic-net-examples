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
            // Add a new page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a graph with specified width and height (double values as required)
            Graph graph = new Graph(400.0, 200.0);

            // Example shape: a rectangle added to the graph
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Graph exported to '{outputPath}'.");
    }
}
