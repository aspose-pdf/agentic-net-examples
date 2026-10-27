using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "gradient_rectangle.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph container that spans the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Parameters for the simulated radial gradient
            int steps = 12;                                 // Number of concentric rectangles
            float centreX = 250f;                           // X‑coordinate of the centre
            float centreY = 450f;                           // Y‑coordinate of the centre
            float maxSize = 300f;                           // Size of the outermost rectangle

            // Build the gradient by drawing several rectangles with decreasing opacity
            for (int i = 0; i < steps; i++)
            {
                // Size shrinks towards the centre
                float size = maxSize * (1f - (float)i / steps);
                float left = centreX - size / 2f;
                float bottom = centreY - size / 2f;

                // Create the rectangle shape
                var rect = new Aspose.Pdf.Drawing.Rectangle(left, bottom, size, size);

                // Calculate opacity – fully opaque at the centre, fully transparent at the outer edge
                int alpha = (int)(255 * (1f - (float)i / steps));

                // Apply fill colour with the calculated alpha value
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.FromArgb(alpha, 255, 0, 0), // Red with variable transparency
                    Color = Color.Transparent,                  // No border colour
                    LineWidth = 0f
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(rect);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
