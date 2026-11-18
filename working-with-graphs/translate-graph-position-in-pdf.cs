using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "translated_graph.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a Graph element with a defined size (use double overload)
            Graph graph = new Graph(200.0, 100.0);

            // Translate the graph: move it 50 points right (horizontal) and 80 points up (vertical)
            // Positioning is done via Left and Top properties (no Transform member exists)
            graph.Left = 50; // horizontal offset
            graph.Top = 80;  // vertical offset

            // Add a rectangle shape to the graph (using the drawing namespace)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 200, 100);
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray; // fill colour
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black;        // border colour (optional)
            rect.GraphInfo.LineWidth = 1;                         // border width (optional)

            graph.Shapes.Add(rect);

            // Place the graph on the page
            page.Paragraphs.Add(graph);

            // Save the PDF to the specified path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
