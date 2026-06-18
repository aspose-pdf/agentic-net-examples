using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a sample PDF with a single blank page (self‑contained)
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // 2. Re‑open the PDF and work with drawing shapes
        // ------------------------------------------------------------
        using (Document doc = new Document("input.pdf"))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // --------------------------------------------------------
            // 3. Define a rectangle that intentionally exceeds the page bounds
            // --------------------------------------------------------
            float rectX = (float)(page.PageInfo.Width - 50);
            float rectY = (float)(page.PageInfo.Height - 50);
            float rectWidth = 200f;
            float rectHeight = 200f;

            // Create the rectangle shape
            Aspose.Pdf.Drawing.Rectangle rectangle = new Aspose.Pdf.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);
            rectangle.GraphInfo = new GraphInfo();
            rectangle.GraphInfo.Color = Aspose.Pdf.Color.Red; // Fixed: use Color.Red property

            try
            {
                // ----------------------------------------------------
                // 4. Manual bounds checking – Aspose.Pdf no longer exposes
                //    UpdateBoundsCheckMode, so we perform the check ourselves.
                //    If the shape does not fit, we throw the same exception
                //    type that Aspose would have thrown.
                // ----------------------------------------------------
                bool exceedsWidth = rectX + rectWidth > page.PageInfo.Width;
                bool exceedsHeight = rectY + rectHeight > page.PageInfo.Height;
                if (exceedsWidth || exceedsHeight)
                {
                    throw new BoundsOutOfRangeException("The rectangle exceeds the page bounds.");
                }

                // ----------------------------------------------------
                // 5. Add the rectangle to a Graph and then to the page.
                //    Graph.Shapes.Add expects a single shape argument.
                // ----------------------------------------------------
                Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(page.PageInfo.Width, page.PageInfo.Height);
                graph.Shapes.Add(rectangle);
                page.Paragraphs.Add(graph);

                Console.WriteLine("Rectangle added successfully.");
            }
            catch (BoundsOutOfRangeException ex)
            {
                // ----------------------------------------------------
                // 6. Detailed logging of the shape coordinates when the
                //    bounds check fails.
                // ----------------------------------------------------
                Console.WriteLine("BoundsOutOfRangeException caught:");
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("Shape: Rectangle");
                Console.WriteLine("Coordinates: X=" + rectX + ", Y=" + rectY + ", Width=" + rectWidth + ", Height=" + rectHeight);
                Console.WriteLine("Container size: Width=" + page.PageInfo.Width + ", Height=" + page.PageInfo.Height);
            }

            // ------------------------------------------------------------
            // 7. Save the resulting PDF (may be unchanged if exception occurred)
            // ------------------------------------------------------------
            doc.Save("output.pdf");
        }
    }
}
