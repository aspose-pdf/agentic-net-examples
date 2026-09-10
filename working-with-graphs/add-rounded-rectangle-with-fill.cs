using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for the output PDF
        const string outputPath = "RoundedRectangle.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts like a canvas) with desired size
            // Width and height can be larger than the rectangle to give some margin
            Graph graph = new Graph(500, 500);

            // Define a rectangle shape with position (left, bottom) and size (width, height)
            // Use the Drawing namespace to avoid ambiguity with Aspose.Pdf.Rectangle (page coordinates)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 400, 200, 100);

            // Set the corner radius for rounded corners
            rectShape.RoundedCornerRadius = 20; // radius in points

            // Define visual appearance via GraphInfo (fill color, border color, line width)
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,   // solid fill color
                Color = Aspose.Pdf.Color.Black,          // border color
                LineWidth = 1                             // border thickness
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rectShape);

            // Add the graph (which now contains the rectangle) to the page's paragraphs
            page.Paragraphs.Add(graph);

            // Save the document to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rounded rectangle saved to '{outputPath}'.");
    }
}