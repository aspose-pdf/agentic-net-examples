using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Output path
        const string outputPath = "shadow_rectangle.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 200pt, height: 100pt)
            // Use double literals as required by the non‑obsolete constructor
            Graph graph = new Graph(200.0, 100.0)
            {
                // Position the graph on the page
                Left = 100,
                Top  = 600
            };

            // ------------------------------------------------------------
            // Shadow effect – emulate a shadow by drawing a second rectangle
            // slightly offset from the main shape and rendered with a
            // semi‑transparent colour.
            // ------------------------------------------------------------

            // Shadow rectangle (offset by 5 points horizontally and vertically)
            Aspose.Pdf.Drawing.Rectangle shadowRect = new Aspose.Pdf.Drawing.Rectangle(5f, 5f, 200f, 100f);
            // Use a semi‑transparent gray to mimic a blurred shadow
            shadowRect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(80, 128, 128, 128);
            // Add the shadow shape first so it appears behind the main shape
            graph.Shapes.Add(shadowRect);

            // Main filled rectangle (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 200f, 100f);
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.LightGray;
            // Add the rectangle on top of the shadow
            graph.Shapes.Add(rect);

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with shadowed rectangle saved to '{outputPath}'.");
    }
}
