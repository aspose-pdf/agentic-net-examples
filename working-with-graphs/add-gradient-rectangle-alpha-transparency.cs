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

            // Create a Graph container (acts as a drawing canvas)
            Graph graph = new Graph(500, 400);
            page.Paragraphs.Add(graph);

            // Define a rectangle shape (position and size)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 300, 400, 100);

            // Create an axial gradient shading: transparent blue -> opaque blue
            GradientAxialShading gradient = new GradientAxialShading(
                Color.FromArgb(0,   0, 0, 255),   // start color: fully transparent blue
                Color.FromArgb(255, 0, 0, 255)    // end color: fully opaque blue
            );

            // Optionally set the start and end points of the gradient
            gradient.Start = new Point(50, 300);
            gradient.End   = new Point(450, 300);

            // Apply the gradient as the fill color of the rectangle
            rect.GraphInfo = new GraphInfo
            {
                FillColor = new Color { PatternColorSpace = gradient }, // gradient fill
                Color     = Aspose.Pdf.Color.Black,                    // rectangle border color
                LineWidth = 1                                            // border thickness
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Save the PDF
            doc.Save("gradient_rectangle.pdf");
        }
    }
}