using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals
            Graph graph = new Graph(500.0, 400.0);

            // Create a rectangle shape inside the graph
            // Fully qualify the drawing rectangle to avoid ambiguity
            Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(
                50.0F,   // left (x)
                300.0F,  // bottom (y)
                200.0F,  // width
                100.0F   // height
            );

            // Set corner radius
            shapeRect.RoundedCornerRadius = 15.0F;

            // Apply a solid fill color and optional border styling via GraphInfo
            shapeRect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.8), // light blue fill
                Color = Aspose.Pdf.Color.Black,                    // border color
                LineWidth = 1.0F                                   // border thickness
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(shapeRect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
    }
}