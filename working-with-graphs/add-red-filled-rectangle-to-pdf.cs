using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container covering the whole page (use double values as required by the constructor)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape using absolute coordinates (left = 100, bottom = 500, width = 200, height = 100)
            // NOTE: Use Aspose.Pdf.Drawing.Rectangle – this type exposes GraphInfo.
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

            // Set visual appearance via GraphInfo (solid red fill, optional black border)
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Red,   // solid fill
                Color = Aspose.Pdf.Color.Black,    // border color
                LineWidth = 1f                     // border thickness
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rectangle saved to '{outputPath}'.");
    }
}
