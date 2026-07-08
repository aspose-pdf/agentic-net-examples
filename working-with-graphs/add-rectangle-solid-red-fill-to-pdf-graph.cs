using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (first page)
            Page page = doc.Pages.Add();

            // Create a Graph container – size can be larger than the rectangle
            // Use the double‑based constructor as the float overload is obsolete
            Graph graph = new Graph(600.0, 800.0);

            // Define a rectangle with absolute coordinates (left = 100, bottom = 400,
            // width = 200, height = 100).  The Drawing.Rectangle expects float values.
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 400f, 200f, 100f);

            // Set visual properties via GraphInfo (solid red fill)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Red,   // solid fill
                Color = Aspose.Pdf.Color.Red,       // border color (optional)
                LineWidth = 1f                      // border thickness (float literal)
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }
    }
}
