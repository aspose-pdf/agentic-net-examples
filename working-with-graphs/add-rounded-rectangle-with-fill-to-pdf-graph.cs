using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based).
            Page page = doc.Pages.Add();

            // Create a Graph container sized to the page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define rectangle position and size (left, bottom, width, height).
            // Original bounds: llx=100, lly=500, urx=300, ury=600 => width=200, height=100.
            Aspose.Pdf.Drawing.Rectangle shape = new Aspose.Pdf.Drawing.Rectangle(100f, 500f, 200f, 100f);

            // Rounded corners.
            shape.RoundedCornerRadius = 10f;

            // Styling via GraphInfo.
            shape.GraphInfo.FillColor = Aspose.Pdf.Color.FromRgb(0.4f, 0.6f, 0.9f); // light blue fill
            shape.GraphInfo.Color = Aspose.Pdf.Color.Black;                     // border color
            shape.GraphInfo.LineWidth = 1f;                                      // border width

            // Add the rectangle to the graph and the graph to the page.
            graph.Shapes.Add(shape);
            page.Paragraphs.Add(graph);

            // Save the document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with rectangle saved to '{outputPath}'.");
    }
}
