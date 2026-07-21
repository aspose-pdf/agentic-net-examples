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
            // Add a blank page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Graph constructor now expects double values
            Graph graph = new Graph(500.0, 500.0); // width and height of the graph area

            // Define a rectangle with absolute coordinates using the drawing rectangle type
            // left = 100, bottom = 400, width = 200, height = 100
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                100f,   // LLX (left)
                400f,   // LLY (bottom)
                200f,   // width
                100f);  // height

            // Set visual appearance via GraphInfo – solid red fill, no visible border
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.Red,   // solid fill
                Color = Color.Red,       // border color (same as fill for consistency)
                LineWidth = 0f           // no border line
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Attach the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}
