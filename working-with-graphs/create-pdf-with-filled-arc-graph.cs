using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph container (width, height in points)
            // Here we set a size that will comfortably contain the arc
            Graph graph = new Graph(200, 200);

            // Define the arc:
            // posX, posY – center of the arc
            // radius – radius of the arc
            // alpha – start angle (degrees)
            // beta  – end angle (degrees)
            Arc arc = new Arc(100f, 100f, 80f, 0f, 180f); // semi‑circle

            // Set visual properties via GraphInfo
            arc.GraphInfo = new GraphInfo
            {
                // Custom fill color (e.g., solid blue)
                FillColor = Color.FromArgb(0, 0, 255)
            };

            // Add the arc to the graph's shape collection
            graph.Shapes.Add(arc);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the PDF to a file
            doc.Save("ArcGraph.pdf");
        }

        Console.WriteLine("PDF with filled arc created successfully.");
    }
}