using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Create a Graph container that spans the whole page (or any size you need)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Create an ellipse: left, bottom, width, height (all as floats)
            // Example: left = 100, bottom = 300, width = 200, height = 200
            Ellipse ellipse = new Ellipse(100f, 300f, 200f, 200f);

            // Set a semi‑transparent fill (alpha = 128 out of 255) – blue color
            ellipse.GraphInfo.FillColor = Color.FromArgb(128, 0, 0, 255);

            // Optional: set a visible border
            ellipse.GraphInfo.Color = Color.Black;
            ellipse.GraphInfo.LineWidth = 1;

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with ellipse saved to '{outputPath}'.");
    }
}
