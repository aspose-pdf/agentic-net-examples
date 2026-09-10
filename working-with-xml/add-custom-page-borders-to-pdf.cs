using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_borders.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Page rectangle (media box) – defines the visible area
                Aspose.Pdf.Rectangle pageRect = page.Rect;

                // Create a Graph container that matches the page size.
                // Width and height are set using float values.
                Graph borderGraph = new Graph((float)pageRect.Width, (float)pageRect.Height)
                {
                    // Position the graph at the lower‑left corner of the page.
                    // The Bottom property does not exist; the default bottom is 0.
                    Left = (float)pageRect.LLX
                };

                // Define a drawing rectangle that covers the whole graph.
                // Use Aspose.Pdf.Drawing.Rectangle (not Aspose.Pdf.Rectangle).
                Aspose.Pdf.Drawing.Rectangle borderRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)pageRect.Width,
                    (float)pageRect.Height);

                // Set visual properties via GraphInfo (transparent fill, visible stroke).
                borderRect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.Transparent,
                    Color = Color.Black,
                    LineWidth = 2f
                };

                // Add the rectangle shape to the graph.
                borderGraph.Shapes.Add(borderRect);

                // Add the graph to the page's paragraph collection.
                page.Paragraphs.Add(borderGraph);
            }

            // Save the modified document as PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPath}'.");
    }
}
