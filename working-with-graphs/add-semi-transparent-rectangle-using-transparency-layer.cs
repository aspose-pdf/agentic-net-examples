using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "transparency_demo.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a Graph that spans the whole page (width, height)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define a semi‑transparent rectangle (50% opacity)
            // Constructor: Rectangle(left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(100f, 300f, 200f, 100f);
            // Use alpha channel for transparency (128 = 50% opacity)
            rect.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255); // semi‑transparent blue
            rect.GraphInfo.Color = Aspose.Pdf.Color.Black; // stroke color
            rect.GraphInfo.LineWidth = 2f;

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Attach the graph to the page content (via Paragraphs collection)
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
