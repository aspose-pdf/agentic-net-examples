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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a Graph container sized to the page dimensions (Graph expects double values)
                Graph graph = new Graph((double)page.Rect.Width, (double)page.Rect.Height);

                // Define a rectangle that matches the page size – use the Drawing.Rectangle type
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.Rect.Width,
                    (float)page.Rect.Height);

                // Set visual properties via GraphInfo (available on Drawing.Rectangle)
                rect.GraphInfo = new GraphInfo
                {
                    // No fill – keep transparent
                    FillColor = Aspose.Pdf.Color.Transparent,
                    // Border color
                    Color = Aspose.Pdf.Color.Black,
                    // Border thickness (float)
                    LineWidth = 2f
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(rect);

                // Add the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPath}'.");
    }
}
