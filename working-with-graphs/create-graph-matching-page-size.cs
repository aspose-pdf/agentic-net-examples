using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is index 1)
            Page page = doc.Pages.Add();

            // Obtain the page dimensions (in points)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph that exactly matches the page size (double constructor)
            Graph graph = new Graph(pageWidth, pageHeight);

            // OPTIONAL: add a sample shape to demonstrate usage
            // Use Aspose.Pdf.Drawing.Rectangle (which exposes GraphInfo) and pass float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                (float)100,                                 // X (left)
                (float)(pageHeight - 200),                  // Y (top) – PDF coordinate system starts at bottom left
                (float)200,                                 // Width
                (float)100);                                // Height

            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}
