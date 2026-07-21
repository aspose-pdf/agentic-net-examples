using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // Define a Graph container (size of the drawing area)
            // Width and height can be adjusted as needed
            Graph graph = new Graph(400, 300);
            page.Paragraphs.Add(graph);

            // Create an unfilled arc
            // Parameters: centerX, centerY, radius, startAngle (alpha), endAngle (beta)
            Arc arc = new Arc(200f, 150f, 100f, 0f, 180f);

            // Set line width
            arc.GraphInfo.LineWidth = 2f;

            // Set dash style (e.g., 5 units dash, 3 units gap)
            arc.GraphInfo.DashArray = new int[] { 5, 3 };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Save the PDF
            doc.Save("ArcExample.pdf");
        }

        Console.WriteLine("PDF with unfilled arc saved as 'ArcExample.pdf'.");
    }
}