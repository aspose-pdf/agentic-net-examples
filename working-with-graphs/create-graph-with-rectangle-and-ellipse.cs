using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a graph container (width: 400pt, height: 300pt) – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 300.0);

            // ----- Rectangle shape (drawing rectangle, not PDF page rectangle) -----
            // Parameters: left, bottom, width, height (float values)
            var rectShape = new Aspose.Pdf.Drawing.Rectangle(50f, 200f, 150f, 100f);
            rectShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray, // light gray fill
                Color = Color.Black,         // black border
                LineWidth = 2f               // border thickness (float)
            };
            graph.Shapes.Add(rectShape);

            // ----- Ellipse shape -----
            // Parameters: left, bottom, width, height (float values)
            var ellipseShape = new Ellipse(250f, 150f, 120f, 80f);
            ellipseShape.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow, // yellow fill
                Color = Color.Red,        // red border
                LineWidth = 1.5f          // border thickness (float)
            };
            graph.Shapes.Add(ellipseShape);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
