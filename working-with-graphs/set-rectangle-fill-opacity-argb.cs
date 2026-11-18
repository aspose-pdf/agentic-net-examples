using System;
using System.IO;
using System.Drawing; // only for verification of ARGB components
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "rectangle_opacity.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph that covers the page (or at least the area we need)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define the rectangle's position and size: left, bottom, width, height
            // Fully qualify the drawing rectangle to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);

            // Set fill color using an ARGB value (alpha = 128 → 50% opacity)
            // Use Aspose.Pdf.Color.FromArgb – not System.Drawing.Color – because GraphInfo expects Aspose.Pdf.Color
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 255, 0, 0);
            // Optional: give the rectangle a visible border
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black; // stroke color
            rect.GraphInfo.LineWidth = 1;                  // border thickness

            // Add the rectangle to the graph, then add the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Verify the color values before saving (convert to System.Drawing.Color to read components)
            System.Drawing.Color fill = rect.GraphInfo.FillColor.ToRgb();
            Console.WriteLine($"Fill Color ARGB: A={fill.A}, R={fill.R}, G={fill.G}, B={fill.B}");
            // Expected output: A=128, R=255, G=0, B=0

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        // Reload the document to demonstrate that it can be opened without errors
        using (Document verifyDoc = new Document(outputPath))
        {
            Console.WriteLine($"Document '{outputPath}' loaded successfully. Page count: {verifyDoc.Pages.Count}");
        }
    }
}
