using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_bounds_check.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Use the double‑based Graph constructor (the float overload is obsolete)
            Graph graph = new Graph(400.0, 200.0)
            {
                Left = 50,   // position from the left edge of the page
                Top  = 600   // position from the bottom edge of the page
            };

            // Enable bounds checking on the Shapes collection.
            // When a shape does not fit within the graph's container,
            // a BoundsNotFitException will be thrown.
            graph.Shapes.UpdateBoundsCheckMode(
                BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                graph.Width,
                graph.Height);

            // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Rectangle
            Aspose.Pdf.Drawing.Rectangle rectFit = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rectFit.GraphInfo = new GraphInfo
            {
                FillColor = Color.LightGray,
                Color     = Color.Black,
                LineWidth = 1f
            };
            graph.Shapes.Add(rectFit);

            // Rectangle that exceeds the graph bounds – this will trigger the exception.
            Aspose.Pdf.Drawing.Rectangle rectOverflow = new Aspose.Pdf.Drawing.Rectangle(350f, 150f, 100f, 100f);
            rectOverflow.GraphInfo = new GraphInfo
            {
                FillColor = Color.Yellow,
                Color     = Color.Red,
                LineWidth = 1f
            };

            try
            {
                graph.Shapes.Add(rectOverflow);
            }
            catch (Exception ex) // BoundsNotFitException is not publicly exposed in all versions; catch generic Exception instead.
            {
                // Handle the bounds violation (log, ignore, etc.)
                Console.WriteLine("Bounds check failed: " + ex.Message);
            }

            // Attach the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
