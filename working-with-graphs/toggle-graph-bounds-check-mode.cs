using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // User setting: true = enforce bounds, false = ignore bounds
        bool enforceBounds = true;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph with specified width and height (points)
            Graph graph = new Graph(400, 200);

            // Toggle the bounds‑check mode of the Shapes collection
            if (enforceBounds)
            {
                // Throw an exception if a shape does not fit within the graph area
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);
            }
            else
            {
                // Default mode – no bounds checking (ignore)
                graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.Default);
            }

            // Example shape: a rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 100, 50);
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.LightGray,
                Color = Aspose.Pdf.Color.Black,
                LineWidth = 1
            };
            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save("output.pdf");
        }

        Console.WriteLine("PDF created with bounds check mode set to " +
                          (enforceBounds ? "ThrowExceptionIfDoesNotFit" : "Default (Ignore)"));
    }
}