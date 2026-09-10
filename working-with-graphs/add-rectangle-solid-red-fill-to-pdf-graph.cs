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
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container sized to the page (float values)
            Graph graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

            // Define a rectangle shape with absolute coordinates:
            // left = 100, bottom = 500, width = 200, height = 100
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

            // Set visual properties via GraphInfo (solid red fill)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Red,
                Color = Aspose.Pdf.Color.Black, // optional stroke color
                LineWidth = 1f                    // optional stroke width
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
