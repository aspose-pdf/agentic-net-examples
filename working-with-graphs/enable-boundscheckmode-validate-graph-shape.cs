using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // BoundsCheckMode enum is here

class Program
{
    static void Main()
    {
        const string outputPath = "graph_boundscheck.pdf";

        // Document lifecycle must be wrapped in a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a Graph with specified width and height (points)
            Graph graph = new Graph(400, 200);
            graph.Left = 50;   // X position on the page
            graph.Top = 600;   // Y position on the page

            // Enable bounds checking: throw an exception if a shape does not fit within the graph
            // The BoundsCheckMode enum lives in Aspose.Pdf.Drawing namespace
            graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);

            // Add a rectangle that fits inside the graph bounds
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            graph.Shapes.Add(rect);

            // Example of a shape that would exceed the bounds and trigger an exception
            // Uncomment the following lines to see the exception in action
            // Aspose.Pdf.Drawing.Rectangle bigRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 500, 300);
            // graph.Shapes.Add(bigRect); // Throws BoundsNotFitException because it doesn't fit

            // Add the graph (with its shapes) to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
