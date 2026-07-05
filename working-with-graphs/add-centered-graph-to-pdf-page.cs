using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "graph_centered.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a Graph with desired width and height (double values as required)
            // Example size: 200x150 points
            Graph graph = new Graph(200.0, 150.0)
            {
                // Center the graph horizontally and vertically on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Add a rectangle shape to the graph to visualize it
            // Use Aspose.Pdf.Drawing.Rectangle (expects float parameters)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 150f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page's Paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with centered graph saved to '{outputPath}'.");
    }
}
