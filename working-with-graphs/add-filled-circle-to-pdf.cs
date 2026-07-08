using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) to hold vector shapes
            Graph graph = new Graph(500, 500); // size of the drawing area

            // Create a filled circle with specified center (250,250) and radius 100
            Circle circle = new Circle(250f, 250f, 100f);

            // Define visual appearance: fill color, border color, and line width
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightBlue,   // fill the circle
                Color = Color.Black,           // border color
                LineWidth = 1                  // border thickness
            };

            // Add the circle to the graph
            graph.Shapes.Add(circle);

            // Add the graph (with the circle) to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }
    }
}