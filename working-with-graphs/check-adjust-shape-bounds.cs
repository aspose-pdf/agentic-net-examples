using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "shape_bounds.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Retrieve page dimensions for bounds checking
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a Graph container sized to the page
            Graph graph = new Graph(pageWidth, pageHeight);

            // Define a rectangle shape that initially lies partially outside the page bounds
            // Constructor: left, bottom, width, height
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(-30, -30, 200, 150);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color = Color.Black,
                LineWidth = 2
            };

            // Verify whether the shape fits within the page dimensions
            bool fits = rectShape.CheckBounds(pageWidth, pageHeight);
            if (!fits)
            {
                // Adjust the shape's position so it fits (move to the page origin)
                rectShape = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 150);
                rectShape.GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 2
                };
            }

            // Add the (potentially adjusted) shape to the graph and the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}