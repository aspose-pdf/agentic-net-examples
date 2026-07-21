using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "shadow_rectangle.pdf";

        // Document lifecycle must be wrapped in a using block (see document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height) – use double literals as required
            Graph graph = new Graph(400.0, 200.0);
            // Position the graph on the page
            graph.Left = 100;
            graph.Top = 500;

            // -----------------------------------------------------------------
            // Simulate a shadow by drawing an offset rectangle behind the main one.
            // Aspose.Pdf.GraphInfo does not expose Shadow* properties, so we create
            // a second rectangle with a semi‑transparent fill and a small offset.
            // -----------------------------------------------------------------
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(5f, -5f, 200f, 100f);
            shadowRect.GraphInfo = new GraphInfo
            {
                // 50% transparent black to mimic a soft shadow
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 0)
            };
            graph.Shapes.Add(shadowRect);

            // Create the filled rectangle shape (left, bottom, width, height)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray // rectangle fill
            };
            // Add the rectangle to the graph (on top of the shadow)
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF (PDF format does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shadowed rectangle saved to '{outputPath}'.");
    }
}
