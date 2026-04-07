using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400pt, height: 200pt) – use double literals as required
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page
                Left = 100,   // X coordinate of the graph's left side
                Top  = 500    // Y coordinate of the graph's top side
            };

            // ------------------------------------------------------------
            // Simulate a shadow by drawing an offset rectangle behind the main one.
            // Aspose.Pdf.GraphInfo does NOT expose Shadow* properties, so we create
            // a second rectangle, shift it, and give it a semi‑transparent fill.
            // ------------------------------------------------------------
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(5f, -5f, 200f, 100f);
            shadowRect.GraphInfo = new GraphInfo
            {
                // 50 % transparent gray shadow
                FillColor = Aspose.Pdf.Color.FromArgb(128, 128, 128, 128)
            };
            graph.Shapes.Add(shadowRect);

            // Create the filled rectangle shape (left: 0, bottom: 0, width: 200pt, height: 100pt)
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray
                // No direct shadow properties – already handled by the offset shape above
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("output.pdf");
        }
    }
}
