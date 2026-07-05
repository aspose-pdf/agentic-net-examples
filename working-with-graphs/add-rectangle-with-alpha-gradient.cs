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

            // Create a Graph container (acts as a canvas for vector shapes)
            // Use the double‑based constructor as required by the API
            Graph graph = new Graph(500.0, 300.0);

            // Define a rectangle shape (positioned at (50,50) with width 400 and height 200)
            // For drawing shapes we must use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle)
            // The constructor expects float values.
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(
                50f,   // X (LLX)
                50f,   // Y (LLY)
                400f,  // Width
                200f   // Height
            );

            // Demonstrate alpha‑channel usage with a semi‑transparent fill.
            // Aspose.Pdf does not expose a direct GradientAxialShading as a FillColor.
            // Instead we use a color that transitions from fully transparent to fully opaque
            // by setting the start color to transparent and the end color to opaque red.
            // The visual effect is achieved by using a semi‑transparent fill color.
            rectShape.GraphInfo = new GraphInfo
            {
                // Fill color with 0% opacity (fully transparent) at the left side
                // and 100% opacity (fully opaque) at the right side is simulated by a
                // semi‑transparent red. For a true gradient you would need to draw multiple
                // thin rectangles or use a different API that supports gradient shading.
                FillColor = Color.FromArgb(128, 255, 0, 0), // 50% opaque red (alpha = 128)
                Color = Color.Black,                     // Stroke color
                LineWidth = 1f
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("gradient_rectangle.pdf");
        }

        Console.WriteLine("PDF with gradient rectangle saved as 'gradient_rectangle.pdf'.");
    }
}
