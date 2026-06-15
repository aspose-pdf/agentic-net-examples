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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions (MediaBox)
                double pageWidth = page.MediaBox.Width;
                double pageHeight = page.MediaBox.Height;

                // Create a Graph container sized to the page
                Graph graph = new Graph(pageWidth, pageHeight);

                // Define a rectangle that matches the page dimensions.
                // Use Aspose.Pdf.Drawing.Rectangle (float parameters) for Graph shapes.
                var borderRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)pageWidth,
                    (float)pageHeight);

                // Set visual properties via GraphInfo (transparent fill, visible stroke)
                borderRect.GraphInfo = new GraphInfo
                {
                    Color = Aspose.Pdf.Color.Black,   // Stroke color
                    LineWidth = 2f,                    // Border thickness (float)
                    FillColor = Aspose.Pdf.Color.Transparent // No fill
                };

                // Add the rectangle shape to the graph
                graph.Shapes.Add(borderRect);

                // Add the graph to the page's paragraphs collection
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with custom borders saved to '{outputPath}'.");
    }
}
