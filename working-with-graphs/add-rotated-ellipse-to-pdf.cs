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

            // Create a Graph container (width, height) to hold vector shapes
            // Use double literals as the Graph constructor now expects double values
            Graph graph = new Graph(400.0, 200.0);

            // Create an ellipse: left = 100, bottom = 100, width = 200, height = 150
            // Ellipse constructor expects float arguments, so use the 'f' suffix
            Ellipse ellipse = new Ellipse(100f, 100f, 200f, 150f);

            // Define visual properties via GraphInfo, including a 45° rotation
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,   // Fill color
                Color = Color.Red,          // Stroke color
                LineWidth = 1.5f,           // Stroke width (float literal)
                RotationAngle = 45f         // Rotate the ellipse 45 degrees (float literal)
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("Ellipse with 45° rotation saved to 'output.pdf'.");
    }
}
