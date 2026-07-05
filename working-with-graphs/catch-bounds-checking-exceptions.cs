using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container for vector shapes.
            // Width and height are arbitrary; they define the drawing canvas.
            Graph graph = new Graph(500, 800);

            // Enable bounds checking so that adding a shape that does not fit
            // within the page dimensions throws BoundsOutOfRangeException.
            // Use the page size as the container dimensions.
            graph.Shapes.UpdateBoundsCheckMode(
                Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                page.PageInfo.Width,
                page.PageInfo.Height);

            try
            {
                // Shape that fits inside the page.
                Aspose.Pdf.Drawing.Rectangle rectInside = new Aspose.Pdf.Drawing.Rectangle(100, 100, 200, 150);
                rectInside.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 1
                };
                graph.Shapes.Add(rectInside);

                // Shape that exceeds the page bounds (will trigger exception).
                // Coordinates are left, bottom, width, height.
                Aspose.Pdf.Drawing.Rectangle rectOutside = new Aspose.Pdf.Drawing.Rectangle(400, 700, 200, 150);
                rectOutside.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.DarkOrange,
                    LineWidth = 2
                };
                graph.Shapes.Add(rectOutside);
            }
            catch (Aspose.Pdf.BoundsOutOfRangeException ex)
            {
                // Log the error with the offending shape's coordinates.
                Console.WriteLine($"BoundsOutOfRangeException: {ex.Message}");
            }

            // Add the graph (with its shapes) to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            string outputPath = "output.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}