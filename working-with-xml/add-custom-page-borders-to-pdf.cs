using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bordered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use Document constructor with file path)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the page rectangle (media box or crop box)
                Aspose.Pdf.Rectangle pageRect = page.Rect;

                // Create a Graph container sized to the page (Graph expects double values)
                Graph graph = new Graph(pageRect.Width, pageRect.Height);

                // Define a drawing rectangle that matches the page size
                // Constructor: (left, bottom, width, height) – all as float
                var borderRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)pageRect.Width,
                    (float)pageRect.Height);

                // Set visual properties via GraphInfo (stroke only, no fill)
                borderRect.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black, // stroke color
                    LineWidth = 2f                    // border thickness
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(borderRect);

                // Position the graph at the page origin – default position is (0,0),
                // so no explicit Bottom property is required (Graph has no Bottom member).
                graph.Left = (float)pageRect.LLX;

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save with file path)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPath}'.");
    }
}
