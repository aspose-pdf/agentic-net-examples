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

        // Wrap the Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Rectangle coordinates (lower‑left x/y, upper‑right x/y)
            float llx = 100f; // left
            float lly = 500f; // bottom
            float urx = 300f; // right
            float ury = 600f; // top
            float offset = 5f; // shadow offset

            // Create a Graph that is at least as large as the page.
            // Shapes (rectangles) must be added to a Graph, then the Graph is added to the page.
            var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

            // ---------- Shadow rectangle (offset, semi‑transparent) ----------
            var shadowRect = new Aspose.Pdf.Drawing.Rectangle(
                llx + offset,   // shifted right
                lly - offset,   // shifted down
                urx + offset,
                ury - offset);

            shadowRect.GraphInfo = new GraphInfo
            {
                // 50 % transparent black fill
                FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 0),
                // No visible border
                Color = Aspose.Pdf.Color.Transparent,
                LineWidth = 0
            };
            graph.Shapes.Add(shadowRect);

            // ---------- Main rectangle ----------
            var mainRect = new Aspose.Pdf.Drawing.Rectangle(llx, lly, urx, ury);
            mainRect.GraphInfo = new GraphInfo
            {
                // Light blue fill (RGB values are 0‑1 range, cast to float)
                FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.6f, 0.9f),
                // Border color and width
                Color = Aspose.Pdf.Color.DarkBlue,
                LineWidth = 1
            };
            graph.Shapes.Add(mainRect);

            // Add the graph (containing both rectangles) to the page.
            page.Paragraphs.Add(graph);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rectangle with shadow saved to '{outputPath}'.");
    }
}
