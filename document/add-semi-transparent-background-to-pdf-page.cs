using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);
        // Target the first page (1‑based indexing)
        Page page = doc.Pages[1];

        // Create a Graph that spans the whole page (Graph constructor expects double values)
        Graph graph = new Graph(page.MediaBox.Width, page.MediaBox.Height);

        // Define a rectangle covering the page area – use fully‑qualified type to avoid ambiguity
        Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
            0f,
            0f,
            (float)page.MediaBox.Width,
            (float)page.MediaBox.Height);

        // Set a semi‑transparent fill color (alpha 128 ≈ 0.5 opacity)
        rect.GraphInfo = new GraphInfo
        {
            FillColor = Color.FromArgb(128, 255, 255, 0) // semi‑transparent yellow
            // Stroke (border) is omitted; defaults to no outline
        };

        // Add the rectangle to the graph
        graph.Shapes.Add(rect);

        // Add the graph to the page's content
        page.Paragraphs.Add(graph);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with background color to '{outputPath}'.");
    }
}