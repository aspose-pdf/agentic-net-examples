using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "combined_graphs.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document())
        {
            // Add a single page to the document
            Aspose.Pdf.Page page = doc.Pages.Add();

            // Define the size of each graph
            double graphWidth = 250.0;
            double graphHeight = 250.0;

            // Calculate positions for a 2x2 grid (left, top)
            // Top‑left
            AddGraph(page, 50.0, page.PageInfo.Height - 50.0 - graphHeight, graphWidth, graphHeight, "Graph 1");
            // Top‑right
            AddGraph(page, 300.0, page.PageInfo.Height - 50.0 - graphHeight, graphWidth, graphHeight, "Graph 2");
            // Bottom‑left
            AddGraph(page, 50.0, page.PageInfo.Height - 300.0 - graphHeight, graphWidth, graphHeight, "Graph 3");
            // Bottom‑right
            AddGraph(page, 300.0, page.PageInfo.Height - 300.0 - graphHeight, graphWidth, graphHeight, "Graph 4");

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }

    // Helper method to create a graph, add a simple shape and a title, and place it on the page
    static void AddGraph(Aspose.Pdf.Page page, double left, double top, double width, double height, string title)
    {
        // Create a Graph container with the specified dimensions
        Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(width, height);
        graph.Left = (float)left;
        graph.Top = (float)top;

        // Example shape: a rectangle that fills the graph area
        Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, (float)width, (float)height);
        rect.GraphInfo = new Aspose.Pdf.GraphInfo
        {
            FillColor = Aspose.Pdf.Color.LightGray,
            Color = Aspose.Pdf.Color.Black,
            LineWidth = 2f
        };
        graph.Shapes.Add(rect);

        // Attach the graph to the page first (so the rectangle is drawn)
        page.Paragraphs.Add(graph);

        // Add a title using a TextFragment positioned relative to the graph's top‑left corner
        Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(title);
        tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
        tf.TextState.FontSize = 14;
        tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
        // Position is absolute on the page: left offset + 5, top offset + (height - 20)
        tf.Position = new Aspose.Pdf.Text.Position((float)left + 5f, (float)(top + height - 20));
        page.Paragraphs.Add(tf);
    }
}
