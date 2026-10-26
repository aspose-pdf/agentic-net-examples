using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

// Alias for the drawing rectangle to avoid confusion with Aspose.Pdf.Rectangle (page bounds)
using DrawRect = Aspose.Pdf.Drawing.Rectangle;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_rectangle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a Graph that spans the whole page (or any size you need)
            Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // Define rectangle dimensions: left, bottom, width, height (all in points)
            // Example: rectangle starting at (100,100) with width 200 and height 100
            DrawRect rect = new DrawRect(100, 100, 200, 100);

            // Set visual appearance via GraphInfo
            rect.GraphInfo = new GraphInfo
            {
                Color = Aspose.Pdf.Color.Red,   // Border color
                LineWidth = 2,
                // Uncomment the next line to add a semi‑transparent fill
                // FillColor = Aspose.Pdf.Color.FromArgb(128, Aspose.Pdf.Color.Yellow)
            };

            // Add the rectangle shape to the graph
            graph.Shapes.Add(rect);

            // Add the graph to the page's paragraph collection
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle added and saved to '{outputPath}'.");
    }
}
