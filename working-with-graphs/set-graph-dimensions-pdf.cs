using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use the double‑parameter constructor (required by the API)
            Graph graph = new Graph(300.0, 200.0);

            // Explicitly set the exact dimensions (optional – the constructor already defines them)
            graph.Width = 300;   // width in points
            graph.Height = 200;  // height in points

            // Create a rectangle shape that fills the graph area
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 300f, 200f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray
            };
            graph.Shapes.Add(rect);

            // Add the configured graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to disk
            doc.Save("graph_dimensions.pdf");
        }
    }
}
