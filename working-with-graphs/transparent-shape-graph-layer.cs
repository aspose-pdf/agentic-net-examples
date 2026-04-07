using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class TransparencyExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (acts as a graphics layer)
            // Use double literals as the constructor now expects double values
            Graph graph = new Graph(500.0, 400.0);

            // Define a semi‑transparent rectangle shape
            // Use Aspose.Pdf.Drawing.Rectangle (the shape type) – it expects float parameters
            var rect = new Aspose.Pdf.Drawing.Rectangle(100f, 200f, 300f, 150f);
            rect.GraphInfo = new GraphInfo
            {
                // Transparency is expressed via the alpha channel (0‑255)
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255), // 50% transparent blue
                Color     = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255), // Stroke color matches fill
                LineWidth = 2f
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraphs collection.
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            string outputPath = "TransparencyLayerExample.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
