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

        // Load the PDF document (using rule: load)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing per rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a Graph container sized to the page (double constructor)
                Graph graph = new Graph((double)page.Rect.Width, (double)page.Rect.Height);

                // Define a rectangle shape that matches the page size using Aspose.Pdf.Drawing.Rectangle
                var border = new Aspose.Pdf.Drawing.Rectangle(
                    (float)page.Rect.LLX,               // left (float)
                    (float)page.Rect.LLY,               // bottom (float)
                    (float)page.Rect.Width,             // width (float)
                    (float)page.Rect.Height);           // height (float)

                // Set visual properties via GraphInfo (stroke color & line width)
                border.GraphInfo = new GraphInfo
                {
                    Color = Color.Black,   // border color
                    LineWidth = 2f          // thickness (float)
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(border);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF (using rule: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPath}'.");
    }
}
