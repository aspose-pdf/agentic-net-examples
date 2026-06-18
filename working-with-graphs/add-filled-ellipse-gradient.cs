using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a sample PDF (self‑contained example)
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // 2. Open the sample PDF and add a graph with a filled ellipse
        // ------------------------------------------------------------
        using (Document pdfDoc = new Document("input.pdf"))
        {
            // Page indexing is 1‑based as required by the rules
            Page page = pdfDoc.Pages[1];

            // -----------------------------------------------------------------
            // Use the Graph constructor that accepts double values (the float ctor
            // is obsolete). The size values are arbitrary – they only need to be
            // large enough to contain the ellipse.
            // -----------------------------------------------------------------
            Graph graph = new Graph(500.0, 400.0);
            graph.Left = 50.0f;
            graph.Top = 500.0f;

            // -----------------------------------------------------------------
            // Create an ellipse shape. The constructor parameters are the left,
            // bottom, width and height of the bounding rectangle (all floats).
            // -----------------------------------------------------------------
            Ellipse ellipse = new Ellipse(100.0f, 100.0f, 200.0f, 150.0f);

            // -----------------------------------------------------------------
            // Configure visual appearance via GraphInfo. The current version of
            // Aspose.Pdf does not expose a FillColorShading property on GraphInfo,
            // therefore we use a solid fill colour. If a future version adds the
            // property, the same code pattern can be applied.
            // -----------------------------------------------------------------
            GraphInfo ellipseInfo = new GraphInfo();
            ellipseInfo.FillColor = Color.LightBlue; // solid fill (gradient not available in this version)
            ellipseInfo.Color = Color.Black;        // stroke colour
            ellipseInfo.LineWidth = 1.0f;
            ellipse.GraphInfo = ellipseInfo;

            // Add the ellipse to the graph (Shapes collection accepts a single argument)
            graph.Shapes.Add(ellipse);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the resulting PDF
            pdfDoc.Save("output.pdf");
        }
    }
}
