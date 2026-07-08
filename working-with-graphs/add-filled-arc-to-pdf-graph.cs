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

            // Create a Graph container (width: 400 points, height: 200 points)
            Graph graph = new Graph(400, 200);

            // Create an Arc shape:
            //   Center at (200, 100), radius 80, start angle 0°, end angle 180°
            Arc arc = new Arc(200f, 100f, 80f, 0f, 180f);

            // Set the fill color of the arc (custom RGB color)
            arc.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromRgb(0.2, 0.5, 0.8) // light teal
            };

            // Add the arc to the graph
            graph.Shapes.Add(arc);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save("ArcGraph.pdf");
        }

        Console.WriteLine("PDF with filled arc created successfully.");
    }
}