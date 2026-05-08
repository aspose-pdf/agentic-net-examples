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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document doc = new Document(inputPath))
        {
            // Apply a visual template to each page
            foreach (Page page in doc.Pages)
            {
                // Create a Graph that covers the whole page (width/height are double as required)
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Add a semi‑transparent background rectangle
                var rect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,                                   // left (float)
                    0f,                                   // bottom (float)
                    (float)page.PageInfo.Width,           // width (float)
                    (float)page.PageInfo.Height);         // height (float)

                // Use Color.FromArgb to embed transparency (alpha 77 ≈ 30% opacity)
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = Color.FromArgb(77, (int)(0.9 * 255), (int)(0.9 * 255), (int)(0.95 * 255)),
                    Color = Color.Gray,               // border color
                    LineWidth = 0.5f
                };
                graph.Shapes.Add(rect);

                // Add the Graph (background) to the page
                page.Paragraphs.Add(graph);

                // Add a header text as part of the template
                TextFragment header = new TextFragment("Custom Template Header");
                header.Position = new Position(50, page.PageInfo.Height - 50); // top‑left position
                header.TextState.FontSize = 24;
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                header.TextState.ForegroundColor = Color.DarkBlue;
                page.Paragraphs.Add(header);
            }

            // Save the modified PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio saved with template to '{outputPath}'.");
    }
}
