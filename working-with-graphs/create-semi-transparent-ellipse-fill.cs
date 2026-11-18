using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_semi_transparent.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a Graph container that will hold drawing primitives
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create an ellipse shape (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Ellipse ellipse = new Ellipse(100f, 500f, 300f, 700f);

            // Set a semi‑transparent fill color (50 % opaque blue)
            // Aspose.Pdf.Color.FromArgb(alpha, red, green, blue) – alpha 0‑255 (0 = fully transparent)
            ellipse.GraphInfo.FillColor = Color.FromArgb(128, 0, 0, 255); // 50 % transparent blue

            // Optionally set a border (stroke) color and width
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1f;

            // Add the ellipse to the graph, then add the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with semi‑transparent ellipse saved to '{outputPath}'.");
    }
}
