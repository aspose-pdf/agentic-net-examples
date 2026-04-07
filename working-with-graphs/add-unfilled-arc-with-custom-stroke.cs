using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "arc_output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Create a Graph container (size large enough for the arc)
            Graph graph = new Graph(400, 400);
            page.Paragraphs.Add(graph); // place the graph on the page

            // Create an unfilled arc:
            //   center at (200,200), radius 100, from 0° to 180°
            Arc arc = new Arc(200, 200, 100, 0, 180);

            // Configure stroke appearance: line width and dash pattern
            arc.GraphInfo = new GraphInfo
            {
                LineWidth = 2f,                 // 2 points line width
                DashArray = new int[] { 5, 3 }  // 5 units on, 3 units off
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Save the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with arc saved to '{outputPath}'.");
    }
}