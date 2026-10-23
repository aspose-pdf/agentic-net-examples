using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ------------------------------------------------------------
            // First layer: semi‑transparent blue rectangle
            // ------------------------------------------------------------
            // Rectangle constructor: (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle blueRect = new Aspose.Pdf.Drawing.Rectangle(100, 400, 200, 100);
            // 50 % opacity blue (alpha = 128)
            blueRect.GraphInfo.FillColor = Color.FromArgb(128, 0, 0, 255);
            // Optional border (set to transparent to hide it)
            blueRect.GraphInfo.Color = Color.Transparent;
            graph.Shapes.Add(blueRect);

            // ------------------------------------------------------------
            // Second layer: semi‑transparent red rectangle overlapping the first
            // ------------------------------------------------------------
            Aspose.Pdf.Drawing.Rectangle redRect = new Aspose.Pdf.Drawing.Rectangle(200, 350, 200, 100);
            // 50 % opacity red (alpha = 128)
            redRect.GraphInfo.FillColor = Color.FromArgb(128, 255, 0, 0);
            redRect.GraphInfo.Color = Color.Transparent;
            graph.Shapes.Add(redRect);

            // Add the graph (containing both rectangles) to the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF; Document.Save(string) writes a PDF regardless of extension
            doc.Save("layered_graphics.pdf");
        }

        Console.WriteLine("PDF with layered graphics created: layered_graphics.pdf");
    }
}
