using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document (self‑contained example, no external input file needed)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph that covers the whole page
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;
            Graph graph = new Graph(pageWidth, pageHeight);

            // Define a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Set a 2‑point border and a dashed line style via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                LineWidth = 2,                         // 2‑point border thickness
                Color     = Aspose.Pdf.Color.Black,    // Border color
                DashArray = new int[] { 3, 3 }         // Dash pattern: 3 points on, 3 points off
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }
    }
}
