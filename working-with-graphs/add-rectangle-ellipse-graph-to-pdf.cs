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

            // Create a graph container (width: 400 points, height: 300 points) – use double literals as required by the new constructor
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle shape -----
            // Parameters: left, bottom, width, height – constructors expect float values
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 150f, 200f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // Light gray fill
                Color = Color.Black,           // Border color
                LineWidth = 2f                 // Border thickness (float literal)
            };
            // Add rectangle to the graph
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height – constructors expect float values
            var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(300f, 150f, 150f, 100f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,      // Yellow fill
                Color = Color.Red,             // Outline color
                LineWidth = 1.5f               // Outline thickness (float literal)
            };
            // Add ellipse to the graph
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF with graph (rectangle + ellipse) created: output.pdf");
    }
}
