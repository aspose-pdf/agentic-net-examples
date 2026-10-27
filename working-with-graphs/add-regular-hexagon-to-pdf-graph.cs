using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Define a Graph that will contain the polygon.
            // Width and height are derived from the original rectangle (200 x 200).
            // Use the double‑based constructor as the float overload is obsolete.
            Graph graph = new Graph(200.0, 200.0)
            {
                Left = 100.0,   // X‑position on the page (llx)
                Top = 500.0     // Y‑position on the page (lly)
            };

            // Create a regular hexagon using a closed Line shape.
            // The first point is repeated at the end to close the shape.
            Line hexagon = new Line(new float[]
            {
                180f, 100f,      // point 1
                140f, 169.28f,   // point 2
                60f,  169.28f,   // point 3
                20f,  100f,      // point 4
                60f,  30.72f,    // point 5
                140f, 30.72f,    // point 6
                180f, 100f       // close back to point 1
            })
            {
                GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,   // Border color
                    LineWidth = 2f                  // Border thickness
                }
            };

            // Add the hexagon to the graph's shape collection
            graph.Shapes.Add(hexagon);

            // Place the graph onto the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with hexagon saved to '{outputPath}'.");
    }
}
