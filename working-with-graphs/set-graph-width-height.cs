using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Use double literals for the Graph constructor (width, height)
            Graph graph = new Graph(200.0, 100.0); // initial size: 200pt x 100pt

            // Set the exact dimensions using the Width and Height properties (double values)
            graph.Width = 300.0;   // desired width in points
            graph.Height = 150.0;  // desired height in points

            // Create a drawing rectangle shape that matches the graph size
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)graph.Width,
                (float)graph.Height);

            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1f
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Insert the graph into the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with configured graph saved to '{outputPath}'.");
    }
}
