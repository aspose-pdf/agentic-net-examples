using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_graph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create a Graph object positioned at (100, 500) using the double‑based constructor
            Graph graph = new Graph(100.0, 500.0);
            // Set the size of the graph directly (Width and Height are double properties)
            graph.Width = 200.0;
            graph.Height = 100.0;

            // Create an ellipse shape that fills the whole graph area
            Ellipse ellipse = new Ellipse(0.0, 0.0, 200.0, 100.0);

            // Configure a solid fill (gradient fill is not available in the current library version)
            ellipse.GraphInfo.FillColor = Color.LightBlue;
            // If a newer Aspose.Pdf version is used, the following properties can be set to obtain a gradient:
            // ellipse.GraphInfo.FillColor2 = Color.DarkBlue;
            // ellipse.GraphInfo.FillMode   = FillMode.LinearGradient;
            // ellipse.GraphInfo.Angle      = 45; // gradient direction in degrees

            // Add the ellipse to the graph container
            graph.Shapes.Add(ellipse);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with filled ellipse saved to '{outputPath}'.");
    }
}
