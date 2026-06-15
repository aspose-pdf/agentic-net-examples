using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class Program
{
    public static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a self‑contained sample PDF (evaluation mode limit: 4 pages)
        // ------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single blank page
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // ------------------------------------------------------------
        // 2. Re‑open the PDF and attempt to add an out‑of‑bounds rectangle
        //    to a Graph. The Graph is configured to throw an exception when
        //    a shape does not fit inside the supplied container dimensions.
        // ------------------------------------------------------------
        using (Document doc = new Document("input.pdf"))
        {
            // 1‑based page indexing as required by Aspose.Pdf
            Page page = doc.Pages[1];

            // Container dimensions – use the page size for bounds checking
            double containerWidth = page.PageInfo.Width;
            double containerHeight = page.PageInfo.Height;

            // Create a Graph with the same size as the page
            Graph graph = new Graph(containerWidth, containerHeight);

            // Enable bounds checking that throws if a shape does not fit
            graph.Shapes.UpdateBoundsCheckMode(
                Aspose.Pdf.BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                containerWidth,
                containerHeight);

            // Define a rectangle that is deliberately placed outside the page bounds
            // Aspose.Pdf.Drawing.Rectangle expects float parameters (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle outOfBoundsRect = new Aspose.Pdf.Drawing.Rectangle(
                (float)(containerWidth + 10),   // left coordinate beyond page width
                (float)(containerHeight + 10), // bottom coordinate beyond page height
                50f,                            // width
                50f);                           // height

            // Optional: give the rectangle a visible style (not required for the exception test)
            outOfBoundsRect.GraphInfo = new GraphInfo
            {
                Color = Color.Transparent,
                FillColor = Color.Transparent,
                LineWidth = 0f
            };

            // Attempt to add the rectangle and verify that the expected exception is thrown
            try
            {
                graph.Shapes.Add(outOfBoundsRect);
                Console.WriteLine("No exception was thrown – test failed.");
            }
            catch (Aspose.Pdf.BoundsOutOfRangeException ex)
            {
                Console.WriteLine("Caught expected BoundsOutOfRangeException: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Any other exception type means the test did not behave as expected
                Console.WriteLine("Caught unexpected exception type: " + ex.GetType().FullName + " – " + ex.Message);
            }

            // Add the graph to the page (it will be empty if the exception was thrown)
            page.Paragraphs.Add(graph);
            doc.Save("output.pdf");
        }
    }
}
