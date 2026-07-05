using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";
        const string outputPath = "portfolio_with_template.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document doc = new Document(inputPath))
        {
            // Apply the visual template to each page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // -----------------------------------------------------------------
                // 1. Create a Graph container that matches the page size.
                // -----------------------------------------------------------------
                // Graph constructor expects double values for width/height.
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // -----------------------------------------------------------------
                // 2. Add a background rectangle (full‑page) with a light fill color.
                //    Use GraphInfo to set visual properties (FillColor, Stroke Color, LineWidth).
                // -----------------------------------------------------------------
                // Drawing.Rectangle expects float parameters, so cast the page dimensions.
                var background = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.PageInfo.Width,
                    (float)page.PageInfo.Height);
                background.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.95), // light gray‑blue
                    Color = Aspose.Pdf.Color.Gray,                     // border color
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(background);

                // -----------------------------------------------------------------
                // 3. Add the Graph to the page's Paragraphs collection.
                // -----------------------------------------------------------------
                page.Paragraphs.Add(graph);

                // -----------------------------------------------------------------
                // 4. Optionally add a header text on top of the template.
                //    TextFragment uses Aspose.Pdf.Text.Position for placement.
                // -----------------------------------------------------------------
                TextFragment header = new TextFragment("Custom Visual Template");
                header.Position = new Position(50, page.PageInfo.Height - 50); // 50 pts from left, 50 pts from top
                header.TextState.FontSize = 24;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio with visual template saved to '{outputPath}'.");
    }
}
