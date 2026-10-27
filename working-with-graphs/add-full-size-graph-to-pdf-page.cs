using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph and drawing shapes live here

class Program
{
    static void Main()
    {
        const string outputPath = "graph_page.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Retrieve the page dimensions via PageInfo (Width/Height are not direct members of Page)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Instantiate a Graph that matches the page size
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
            // Style the rectangle through its GraphInfo property
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;   // Fill colour
            rect.GraphInfo.Color     = Aspose.Pdf.Color.DarkGray;    // Border colour
            rect.GraphInfo.LineWidth = 1;                           // Border width

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with Graph saved to '{outputPath}'.");
    }
}
