using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPdfPath = "EllipseWithPattern.pdf";

        // Create the main PDF document.
        using (Document doc = new Document())
        {
            // Add a page that will host the drawing.
            Page page = doc.Pages.Add();

            // Create a Graph container – the only object that can be added to page.Paragraphs.
            // The Graph size is set to the page size so that coordinates match the page.
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the ellipse bounds (left, bottom, width, height).
            // Here we create a 200x200 ellipse positioned at (100, 500).
            Ellipse ellipse = new Ellipse(100, 500, 200, 200);

            // Set the fill and stroke using GraphInfo – Aspose.Pdf does not expose brush objects.
            // For demonstration we use a solid colour; a true pattern fill would require a custom
            // image background or low‑level PDF operators, which are outside the scope of the core API.
            ellipse.GraphInfo.FillColor = Color.Red;   // Fill colour (simulating a pattern).
            ellipse.GraphInfo.Color = Color.Black;    // Border colour.
            ellipse.GraphInfo.LineWidth = 1;          // Border thickness.

            // Add the ellipse to the Graph, then add the Graph to the page.
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the resulting PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF created: {outputPdfPath}");
    }
}
