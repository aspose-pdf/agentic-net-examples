using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_graph.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();
            Page page = doc.Pages[1];

            // Create a graph container (width, height in points)
            Graph graph = new Graph(400, 400)
            {
                // Position the graph on the page (optional)
                Left = 100,
                Top  = 500
            };

            // Create an arc: center (200,200), radius 100, from 0° to 180°
            Arc arc = new Arc(200, 200, 100, 0, 180)
            {
                // Set visual properties via GraphInfo
                GraphInfo = new GraphInfo
                {
                    // Fill the arc with opaque red
                    FillColor = Color.FromArgb(255, 255, 0, 0)
                }
            };

            // Add the arc shape to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}