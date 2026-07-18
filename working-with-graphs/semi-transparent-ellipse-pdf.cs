using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "ellipse_semi_transparent.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page
            Page page = doc.Pages.Add();

            // Use the Graph constructor that accepts double values
            Graph graph = new Graph(500.0, 400.0); // width, height as double

            // Ellipse also expects double parameters (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100.0, 150.0, 300.0, 200.0);

            // Set a semi‑transparent fill colour.  Alpha is an int 0‑255 (128 ≈ 50 % opacity).
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0), // 50 % transparent red
                Color     = Color.Black,                  // stroke colour
                LineWidth = 1f                           // float for line width
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with semi‑transparent ellipse saved to '{outputPath}'.");
    }
}
