using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "layered_graphics.pdf";

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // OPTIONAL: Enable transparency group for the page (required for proper blending)
            // The Group class requires the owning page as a constructor argument.
            page.Group = new Group(page);

            // -----------------------------------------------------------------
            // Draw a semi‑transparent red rectangle.
            // -----------------------------------------------------------------
            // Create a Graph container (acts as a drawing canvas). Use double literals as required.
            Graph redGraph = new Graph(500.0, 500.0);
            // Define a rectangle shape (left, bottom, width, height) – use the Drawing.Rectangle type.
            var redRect = new Aspose.Pdf.Drawing.Rectangle(100f, 300f, 200f, 150f);
            // Set visual properties via GraphInfo. Transparency is achieved with an ARGB color where the
            // alpha component (0‑255) defines opacity (128 = 50 %).
            redRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0), // 50 % transparent red
                Color     = Color.Red,                     // stroke color
                LineWidth = 2f
            };
            redGraph.Shapes.Add(redRect);
            // Add the drawing to the page.
            page.Paragraphs.Add(redGraph);

            // -----------------------------------------------------------------
            // Draw a semi‑transparent blue rectangle that overlaps the red one.
            // -----------------------------------------------------------------
            Graph blueGraph = new Graph(500.0, 500.0);
            var blueRect = new Aspose.Pdf.Drawing.Rectangle(200f, 250f, 200f, 150f);
            blueRect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 0, 0, 255), // 50 % transparent blue
                Color     = Color.Blue,
                LineWidth = 2f
            };
            blueGraph.Shapes.Add(blueRect);
            page.Paragraphs.Add(blueGraph);

            // -----------------------------------------------------------------
            // Save the document as a PDF file.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with layered graphics saved to '{outputPath}'.");
    }
}
