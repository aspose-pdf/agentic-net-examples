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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph object (width: 400 points, height: 200 points) using double constructor
            Graph graph = new Graph(400.0, 200.0);

            // Define a rectangle shape inside the graph (float parameters)
            var rect = new Aspose.Pdf.Drawing.Rectangle(50f, 50f, 150f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                // Semi‑transparent fill (50% opacity)
                FillColor = Color.FromArgb(128, 51, 153, 204), // 0.2,0.6,0.8 with 50% alpha
                // Stroke color of the rectangle border
                Color = Color.Black,
                // Border line width
                LineWidth = 2f
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph (with the semi‑transparent shape) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("output.pdf");
        }
    }
}
