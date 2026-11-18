using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Alias to avoid ambiguity between PDF page rectangle and drawing rectangle
using PdfRect = Aspose.Pdf.Rectangle;
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string outputPath = "clipped_graph.pdf";

        // Document lifecycle must be wrapped in a using block (document-disposal-with-using rule)
        using (Document doc = new Document())
        {
            // Add a new page (Pages.Add returns the newly created page)
            Page page = doc.Pages.Add();

            // Define the clipping region as a rectangle on the page.
            // Since Graph does not expose a Clip property, we position the Graph
            // at the desired location and give it the same size as the clipping region.
            // Anything drawn outside the Graph bounds will not be rendered.
            const double clipLeft = 100;
            const double clipBottom = 100;
            const double clipWidth = 300;   // 400 - 100
            const double clipHeight = 300;  // 400 - 100

            // Create a Graph object using the double‑precision constructor (Graph(float,float) is obsolete).
            Graph graph = new Graph(clipWidth, clipHeight)
            {
                Left = clipLeft,
                Top = clipBottom   // Top is measured from the bottom of the page in Aspose.Pdf
            };

            // Draw a large rectangle that exceeds the clipping bounds.
            // Coordinates are relative to the Graph (0,0) = bottom‑left of the Graph.
            DrawRect largeRect = new DrawRect( -50, 350, 500, 200)
            {
                GraphInfo = { FillColor = Aspose.Pdf.Color.LightGray }
            };
            graph.Shapes.Add(largeRect);

            // Draw a large ellipse that also exceeds the clipping bounds.
            Ellipse largeEllipse = new Ellipse( -100, 400, 600, 200)
            {
                GraphInfo = { FillColor = Aspose.Pdf.Color.LightBlue }
            };
            graph.Shapes.Add(largeEllipse);

            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Save the document as PDF (no special SaveOptions needed for PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with clipped graph saved to '{outputPath}'.");
    }
}
