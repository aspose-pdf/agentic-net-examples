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

            // Define a Graph container (size can be larger than the circle)
            Graph graph = new Graph(500, 500);

            // Specify circle center (X, Y) and radius
            float centerX = 250f;   // X-coordinate of center
            float centerY = 250f;   // Y-coordinate of center
            float radius  = 100f;   // Radius of the circle

            // Create the circle shape
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual properties via GraphInfo (filled with light blue, black border)
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,
                Color     = Color.Black,
                LineWidth = 2
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with filled circle saved as 'output.pdf'.");
    }
}