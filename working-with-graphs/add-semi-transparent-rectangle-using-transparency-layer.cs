using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // Begin a transparency layer by creating a Graph (paragraph)
            // that will contain the drawing commands.
            // ------------------------------------------------------------
            // Graph constructor expects width and height (points)
            Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(400, 200);

            // Create a rectangle shape (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 300, 100);

            // Set visual properties via GraphInfo.
            // Use a semi‑transparent fill by combining a color with the
            // Artifact.Opacity property (the shape itself does not expose
            // opacity, so we use an Artifact wrapper).
            rect.GraphInfo = new Aspose.Pdf.GraphInfo
            {
                // Fill with a light red color
                FillColor = Aspose.Pdf.Color.FromRgb(1.0, 0.6, 0.6),
                // Outline color
                Color = Aspose.Pdf.Color.Red,
                LineWidth = 2
            };

            // Add the rectangle to the graph's shape collection
            graph.Shapes.Add(rect);

            // ------------------------------------------------------------
            // End of transparency layer – the graph is now a complete
            // paragraph that can be added to the page.
            // ------------------------------------------------------------
            // Add the graph to the page's paragraphs collection
            page.Paragraphs.Add(graph);

            // Optional: flatten transparency so that the semi‑transparent
            // appearance is rasterized into the final PDF.
            doc.FlattenTransparency();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with semi‑transparent shape saved to '{outputPath}'.");
    }
}