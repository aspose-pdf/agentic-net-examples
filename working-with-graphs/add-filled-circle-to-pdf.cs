using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "circle.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container that covers the whole page
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the circle's center coordinates and radius
            float centerX = 200f;   // X-coordinate of the center
            float centerY = 400f;   // Y-coordinate of the center
            float radius  = 50f;    // Radius of the circle

            // Instantiate the Circle shape
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual styling via GraphInfo (filled circle with outline)
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue, // Fill color
                Color     = Color.DarkBlue,  // Outline color
                LineWidth = 2                // Outline thickness
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled circle saved to '{outputPath}'.");
    }
}