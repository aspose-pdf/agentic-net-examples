using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class VectorGraphicsDemo
{
    static void Main()
    {
        // Input PDF containing vector graphics
        const string inputPdfPath = "input.pdf";

        // Directory where extracted SVG files will be saved
        const string svgOutputDir = "ExtractedSvg";

        // Output PDF after adding new vector graphics
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the SVG output directory exists
        Directory.CreateDirectory(svgOutputDir);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // ------------------------------------------------------------
                // 1. Detect existing vector graphics and export them as SVG files
                // ------------------------------------------------------------
                if (page.HasVectorGraphics())
                {
                    string svgPath = System.IO.Path.Combine(svgOutputDir, $"page_{pageIndex}.svg");
                    bool saved = page.TrySaveVectorGraphics(svgPath);
                    Console.WriteLine(saved
                        ? $"Vector graphics from page {pageIndex} saved to '{svgPath}'."
                        : $"Failed to save vector graphics from page {pageIndex}.");
                }

                // ------------------------------------------------------------
                // 2. Add new vector graphics (a rectangle) using the Graph API
                // ------------------------------------------------------------
                // Create a Graph that matches the page size
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define a rectangle shape (coordinates: left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
                shapeRect.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };

                // Add the rectangle to the graph
                graph.Shapes.Add(shapeRect);

                // Attach the graph to the page's paragraph collection
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
        }
    }
}