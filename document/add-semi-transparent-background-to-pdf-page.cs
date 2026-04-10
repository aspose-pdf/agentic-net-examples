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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Determine page size
            double pageWidth = page.Rect.Width;
            double pageHeight = page.Rect.Height;

            // Create a Graph container that matches the page size
            Graph graph = new Graph(pageWidth, pageHeight);

            // Create a rectangle that covers the whole page (use Drawing.Rectangle, not Pdf.Rectangle)
            var rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)pageHeight);

            // Set fill color with alpha (opacity). 128 = 50% transparent.
            rect.GraphInfo = new GraphInfo
            {
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255), // semi‑transparent blue
                Color = Aspose.Pdf.Color.Transparent, // no border
                LineWidth = 0f
            };

            // Add the rectangle to the graph
            graph.Shapes.Add(rect);

            // Insert the graph as the first paragraph so it appears behind existing content
            page.Paragraphs.Insert(0, graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background color to '{outputPath}'.");
    }
}
