using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure it is disposed properly
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph container that matches the page size (double values are required)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a rectangle shape; coordinates may place it outside the page bounds
            // Graph shapes must be created with Aspose.Pdf.Drawing.Rectangle (float parameters)
            float left = 500f;
            float bottom = 700f;
            float width = 200f;
            float height = 150f;

            var rectShape = new Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.LightGray,
                    Color = Color.Black,
                    LineWidth = 2f
                }
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rectShape);
            page.Paragraphs.Add(graph);

            // Retrieve page dimensions for bounds checking
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Manual bounds check – Aspose.Pdf.Rectangle does not expose a CheckBounds method
            bool fits = left >= 0 && bottom >= 0 &&
                        left + width <= pageWidth &&
                        bottom + height <= pageHeight;

            if (!fits)
            {
                // Calculate new left (LLX) ensuring the shape stays within the page horizontally
                float newLeft = (float)Math.Max(0, Math.Min(left, pageWidth - width));
                // Calculate new bottom (LLY) ensuring the shape stays within the page vertically
                float newBottom = (float)Math.Max(0, Math.Min(bottom, pageHeight - height));

                // Create a new rectangle with the adjusted position, preserving original styling
                var adjusted = new Aspose.Pdf.Drawing.Rectangle(newLeft, newBottom, width, height)
                {
                    GraphInfo = rectShape.GraphInfo
                };

                // Replace the original shape with the adjusted one
                graph.Shapes.Remove(rectShape);
                graph.Shapes.Add(adjusted);
            }

            // Save the resulting PDF
            doc.Save("output.pdf");
        }
    }
}
