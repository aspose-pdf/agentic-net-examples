using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

public class WatermarkArtifact
{
    /// <summary>
    /// Applies a tiled semi‑transparent rectangle as a background watermark to every page of the input PDF.
    /// </summary>
    /// <param name="inputPdfPath">Path to the source PDF.</param>
    /// <param name="outputPdfPath">Path where the watermarked PDF will be saved.</param>
    public void Apply(string inputPdfPath, string outputPdfPath)
    {
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over each page and add a tiled pattern made of semi‑transparent rectangles.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Create a Graph that covers the whole page.
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Tile size (in points).
                const float tileWidth = 100f;
                const float tileHeight = 100f;
                const float rectMargin = 10f; // margin inside each tile

                // Loop over the page surface and place a rectangle in each tile.
                for (float y = 0; y < page.PageInfo.Height; y += tileHeight)
                {
                    for (float x = 0; x < page.PageInfo.Width; x += tileWidth)
                    {
                        // Rectangle positioned inside the current tile.
                        // Constructor: left, bottom, width, height
                        Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                            x + rectMargin,
                            y + rectMargin,
                            tileWidth - 2 * rectMargin,
                            tileHeight - 2 * rectMargin);

                        // Styling via GraphInfo – use semi‑transparent light gray.
                        rect.GraphInfo = new GraphInfo
                        {
                            FillColor = Color.FromArgb(128, 230, 230, 230), // 50% opacity
                            Color = Color.Transparent, // No border
                            LineWidth = 0
                        };

                        graph.Shapes.Add(rect);
                    }
                }

                // Add the graph (containing the tiled rectangles) to the page.
                page.Paragraphs.Add(graph);
            }

            // Save the modified document.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }

    // ---------------------------------------------------------------------
    // Entry point – required for a console‑application build.
    // ---------------------------------------------------------------------
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: WatermarkArtifact <input.pdf> <output.pdf>");
            return;
        }

        var artifact = new WatermarkArtifact();
        artifact.Apply(args[0], args[1]);
    }
}
