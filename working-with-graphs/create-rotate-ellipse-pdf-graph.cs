using System;
using System.IO;
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

            // Create a Graph container (size can be adjusted as needed)
            Graph graph = new Graph(400, 200);

            // Create an ellipse: left = 100, bottom = 100, width = 200, height = 150
            Ellipse ellipse = new Ellipse(100, 100, 200, 150);

            // Set visual properties and rotate the ellipse by 45 degrees
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,   // Fill color
                Color = Color.Red,          // Border color
                LineWidth = 2,              // Border thickness
                RotationAngle = 45          // Rotation angle in degrees
            };

            // Add the ellipse to the graph
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("EllipseRotated45.pdf");
        }

        Console.WriteLine("PDF with rotated ellipse saved as 'EllipseRotated45.pdf'.");
    }
}