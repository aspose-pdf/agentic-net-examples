using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Operators;

class DrawLineExample
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define a Graph container (size does not affect the line coordinates)
            // Use the double‑based constructor as the float overload is obsolete.
            Graph graph = new Graph(500.0, 200.0);

            // Define start and end points for the line (x1, y1, x2, y2)
            float[] linePoints = { 50f, 150f, 450f, 150f };
            Line line = new Line(linePoints);

            // Configure visual appearance of the line
            line.GraphInfo = new GraphInfo
            {
                Color = Color.Blue,      // line color
                LineWidth = 3f           // line thickness (float literal)
            };

            // Add the line shape to the graph
            graph.Shapes.Add(line);

            // Set line cap style for the page content (e.g., round caps)
            // In recent Aspose.PDF versions the operator collection is accessed via the `Contents` property.
            page.Contents.Add(new SetLineCap(LineCap.RoundCap));

            // Add the graph (which contains the line) to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            string outputPath = "LineWithCap.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}
