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

        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a Graph that spans the whole page – Graph is the container for drawing shapes
            Graph graph = new Graph((float)page.PageInfo.Width, (float)page.PageInfo.Height);

            // Create a rectangle that covers the whole page (left, bottom, width, height)
            var background = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)page.PageInfo.Width,
                (float)page.PageInfo.Height);

            // Initialise GraphInfo and set a semi‑transparent fill colour (30 % opacity)
            background.GraphInfo = new GraphInfo();
            // 30 % opacity => alpha ≈ 0.3 × 255 ≈ 76
            background.GraphInfo.FillColor = Aspose.Pdf.Color.FromArgb(76, 230, 230, 230);

            // Add the rectangle to the graph, then add the graph to the page
            graph.Shapes.Add(background);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background color added and saved to '{outputPath}'.");
    }
}
