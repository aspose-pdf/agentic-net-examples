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
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Use the double‑based Graph constructor (the float overload is obsolete)
            Graph graph = new Graph(500.0, 400.0);

            // Create an ellipse shape (left, bottom, width, height)
            Ellipse ellipse = new Ellipse(100, 200, 300, 150);

            // Set a semi‑transparent fill colour. Aspose.Pdf.Drawing.Color provides an
            // ARGB factory method – the first argument is the alpha channel (0‑255).
            // 128 gives 50 % opacity.
            ellipse.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 255, 0, 0) // 50 % transparent red
            };

            // Add the ellipse to the graph and the graph to the page
            graph.Shapes.Add(ellipse);
            page.Paragraphs.Add(graph);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Ellipse with semi‑transparent fill saved to '{outputPath}'.");
    }
}
