using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_with_shadow.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph container (200x200 points) and position it on the page
            Graph graph = new Graph(200.0, 200.0)
            {
                Left = 100,
                Top = 400
            };

            // -----------------------------------------------------------------
            // Shadow emulation: Aspose.Pdf.GraphInfo does not expose a Shadow
            // property, so we draw a second rectangle slightly offset and with
            // a semi‑transparent fill to mimic a blurred shadow.
            // -----------------------------------------------------------------
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                20 + 5,   // X position + horizontal offset
                20 - 5,   // Y position + vertical offset (negative moves up)
                160 + 5, // Width + horizontal offset
                100 - 5   // Height + vertical offset
            )
            {
                GraphInfo = new GraphInfo
                {
                    // 30% opaque black (alpha 80 out of 255) creates a soft shadow
                    FillColor = Color.FromArgb(80, 0, 0, 0)
                }
            };

            // Main filled rectangle inside the graph
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(20, 20, 160, 100)
            {
                GraphInfo = new GraphInfo
                {
                    FillColor = Color.FromRgb(0.2, 0.6, 0.8) // rectangle fill color
                }
            };

            // Add the shadow first so it is rendered beneath the main shape
            graph.Shapes.Add(shadowRect);
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
