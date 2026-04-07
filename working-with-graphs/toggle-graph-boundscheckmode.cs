using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // Graph, BoundsCheckMode, GraphInfo, Rectangle

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // User setting: true = strict (throw if shape doesn't fit), false = ignore bounds
        bool useStrictBoundsCheck = true;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width: 400, height: 200) – use double literals as required by the constructor
            Graph graph = new Graph(400.0, 200.0);

            // Add a rectangle shape to the graph – Rectangle expects float arguments
            var rect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 100f, 50f);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray
            };
            graph.Shapes.Add(rect);

            // Toggle the bounds‑check mode based on the user setting
            if (useStrictBoundsCheck)
            {
                // Throw BoundsNotFitException if an item doesn't fit
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);
            }
            else
            {
                // When not strict, simply do not change the mode – the default behaviour is to ignore bounds.
                // The non‑strict enum value (IgnoreIfDoesNotFit) does not exist in the current Aspose.PDF version,
                // so we rely on the default "ignore" behaviour.
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
