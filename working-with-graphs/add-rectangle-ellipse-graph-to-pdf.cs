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

            // Create a graph container with desired size (width, height)
            // Graph constructor now expects double values
            Graph graph = new Graph(400.0, 200.0); // 400pt wide, 200pt high

            // ---------- Rectangle shape ----------
            // Constructor: left, bottom, width, height (expects float)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 100f, 150f, 80f);
            // Set visual properties via GraphInfo
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,   // Fill color
                Color = Color.DarkBlue,        // Stroke color
                LineWidth = 2f                 // Stroke width (float literal)
            };
            // Add rectangle to the graph
            graph.Shapes.Add(rectShape);

            // ---------- Ellipse shape ----------
            // Constructor: left, bottom, width, height (expects float)
            var ellipseShape = new Aspose.Pdf.Drawing.Ellipse(250f, 100f, 120f, 80f);
            // Set visual properties via GraphInfo
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,      // Fill color
                Color = Color.Red,             // Stroke color
                LineWidth = 1.5f               // Stroke width (float literal)
            };
            // Add ellipse to the graph
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}