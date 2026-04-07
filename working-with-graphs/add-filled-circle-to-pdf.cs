using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "filled_circle.pdf";

        // Create a new PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document())
        {
            // Add a page first – a newly created Document has no pages.
            Page page = doc.Pages.Add();

            // Create a Graph container – use the double‑based constructor as the float one is obsolete.
            Graph graph = new Graph(500.0, 500.0); // width, height in points (double literals)

            // Define circle parameters (center X, center Y, radius) – use float values for the Circle constructor.
            float centerX = 250f;
            float centerY = 250f;
            float radius   = 100f;

            // Instantiate the Circle shape (float overload is required).
            Circle circle = new Circle(centerX, centerY, radius);

            // Set visual styling via GraphInfo (fill and stroke colors, line width).
            circle.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightBlue, // fill color
                Color     = Aspose.Pdf.Color.DarkBlue,  // outline color
                LineWidth = 2f                         // float literal for line width
            };

            // Add the circle to the graph.
            graph.Shapes.Add(circle);

            // Add the graph to the page's paragraph collection.
            page.Paragraphs.Add(graph);

            // Save the document to the specified path.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with a filled circle saved to '{outputPath}'.");
    }
}
