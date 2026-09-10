using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "shadow_rectangle.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Graph container (width, height)
            Graph graph = new Graph(400.0, 200.0);

            // ----- Shadow rectangle (offset +5, -5) -----
            // In PDF coordinate system the origin is bottom‑left, so a negative Y offset moves the shape down.
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                55,   // left  = original left  + OffsetX (5)
                95,   // bottom = original bottom - OffsetY (5)
                250,  // width
                50    // height
            );
            shadowRect.GraphInfo = new GraphInfo
            {
                // Semi‑transparent gray to simulate a soft shadow
                FillColor = Aspose.Pdf.Color.FromArgb(128, 200, 200, 200)
            };
            graph.Shapes.Add(shadowRect);

            // ----- Main filled rectangle -----
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 100, 250, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray
            };
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shadowed rectangle saved to '{outputPath}'.");
    }
}
