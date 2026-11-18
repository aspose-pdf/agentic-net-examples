using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "graph.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height)
            Graph graph = new Graph(300f, 200f);
            // Position the graph on the page (coordinates are measured from the bottom‑left corner of the page)
            graph.Left = 100f;   // distance from the left edge of the page
            graph.Top = 500f;    // distance from the bottom edge of the page

            // Create an Arc shape
            Arc arc = new Arc(150f, 150f, 80f, 0f, 180f);
            // Fill the arc with a custom color
            arc.GraphInfo.FillColor = Color.FromRgb(0.2, 0.6, 0.8);
            // Outline the arc with a black pen (line width 1)
            arc.GraphInfo.Color = Color.Black;
            arc.GraphInfo.LineWidth = 1f;

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with graph saved to '{outputPath}'.");
    }
}
