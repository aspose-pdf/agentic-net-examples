using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_bounds_check.pdf";

        // Ensure the output directory exists
        string outputDir = System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph with a specific size (e.g., 400x200 points)
            Graph graph = new Graph(400.0, 200.0)
            {
                // Position the graph on the page
                Left = 50,
                Top = 600,
                // Optional visual styling – use BorderInfo constructor
                Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black)
            };

            // Enable bounds checking: throw if a shape does not fit within the graph area
            graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);

            // Add a rectangle that fits within the graph (should succeed)
            Aspose.Pdf.Drawing.Rectangle rectInside = new Aspose.Pdf.Drawing.Rectangle(10, 10, 100, 50);
            rectInside.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black
            };
            graph.Shapes.Add(rectInside);

            // Add a rectangle that exceeds the graph bounds (will trigger exception)
            Aspose.Pdf.Drawing.Rectangle rectOutside = new Aspose.Pdf.Drawing.Rectangle(350, 150, 100, 100); // extends beyond 400x200
            rectOutside.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.Yellow,
                Color = Aspose.Pdf.Color.Red
            };
            try
            {
                graph.Shapes.Add(rectOutside);
            }
            catch (Exception ex)
            {
                // Expected: BoundsNotFitException (or generic Exception) because the shape does not fit
                Console.WriteLine($"Bounds check failed: {ex.Message}");
            }

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
