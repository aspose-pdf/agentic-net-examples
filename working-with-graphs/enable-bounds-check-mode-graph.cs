using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "graph_output.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a Graph with width and height
            Graph graph = new Graph(500.0, 500.0);

            // Enable bounds checking on the Shapes collection
            graph.Shapes.UpdateBoundsCheckMode(BoundsCheckMode.ThrowExceptionIfDoesNotFit);

            // Add a rectangle that fits inside the graph bounds (use the drawing Rectangle type)
            Aspose.Pdf.Drawing.Rectangle drawRect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 200);
            graph.Shapes.Add(drawRect);

            // Uncomment the following line to test the bounds check (this ellipse exceeds the graph area and will cause an exception)
            // Aspose.Pdf.Drawing.Ellipse drawEllipse = new Aspose.Pdf.Drawing.Ellipse(400, 400, 600, 600);
            // graph.Shapes.Add(drawEllipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
