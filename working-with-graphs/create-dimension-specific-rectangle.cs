using System;
using System.IO;
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

            // Define the size of the graph (canvas) in points
            // Here we create a graph of 400x200 points
            Graph graph = new Graph(400, 200);

            // Position the graph on the page (optional)
            // By default the graph is placed at the origin (0,0) of the page.
            // You can move it by setting its Left and Bottom properties if needed:
            // graph.Left = 50;
            // graph.Bottom = 500;

            // Create a rectangle shape with specific dimensions
            // Parameters: left, bottom, width, height (all in points)
            Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(50, 30, 200, 100);

            // Set visual properties via GraphInfo
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray, // Fill color
                Color = Aspose.Pdf.Color.Black,         // Stroke color
                LineWidth = 2                            // Border thickness
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rectShape);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("DimensionSpecificRectangle.pdf");
        }

        Console.WriteLine("PDF with dimension‑specific rectangle created successfully.");
    }
}