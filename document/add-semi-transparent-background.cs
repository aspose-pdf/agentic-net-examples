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
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Determine page dimensions
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Create a Graph that covers the whole page
            Graph graph = new Graph((float)pageWidth, (float)pageHeight);

            // Create a drawing rectangle that fills the page and set a semi‑transparent fill color
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                0f,
                0f,
                (float)pageWidth,
                (float)pageHeight);

            rect.GraphInfo = new GraphInfo
            {
                FillColor = Color.FromArgb(128, 0, 0, 255), // 50% transparent blue
                Color = Color.Transparent,                // No stroke color
                LineWidth = 0f
            };

            // Add the rectangle to the graph and the graph to the page
            graph.Shapes.Add(rect);
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF with background rectangle to '{outputPath}'.");
    }
}
