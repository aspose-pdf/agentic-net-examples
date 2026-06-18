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
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define page margins (in points)
            page.PageInfo.Margin = new MarginInfo
            {
                Top = 50,
                Bottom = 50,
                Left = 40,
                Right = 40
            };

            // Create a Graph object (width, height) that will be placed in the footer
            // NOTE: Graph constructor now expects double values
            Graph graph = new Graph(200.0, 30.0); // 200pt wide, 30pt high

            // Add a simple rectangle shape to the graph
            // Rectangle constructor expects float values
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 30f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 1f // float literal
            };
            graph.Shapes.Add(rect);

            // Initialize the page footer
            page.Footer = new HeaderFooter();

            // Optional: set footer margins (relative to page margins)
            page.Footer.Margin = new MarginInfo
            {
                Bottom = 10 // extra space from the bottom edge of the page
            };

            // Add the graph to the footer's paragraph collection
            page.Footer.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("GraphInFooter.pdf");
        }

        Console.WriteLine("PDF with graph in footer created successfully.");
    }
}
