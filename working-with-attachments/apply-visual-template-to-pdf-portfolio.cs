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

                // Add a semi‑transparent header rectangle
                var headerRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,                                                       // left (float)
                    (float)(page.PageInfo.Height - 50),                      // bottom (float) – 50 units from top
                    (float)page.PageInfo.Width,                               // width (float)
                    50f);                                                     // height (float)

                headerRect.GraphInfo = new GraphInfo
                {
                    // 50% transparent blue using ARGB (alpha 128)
                    FillColor = Aspose.Pdf.Color.FromArgb(128, 0, 102, 204),
                    Color = Aspose.Pdf.Color.White, // border color (not visible because LineWidth = 0f)
                    LineWidth = 0f
                };
                graph.Shapes.Add(headerRect);

                // Add the Graph to the page
                page.Paragraphs.Add(graph);

                // Add a title text inside the header
                TextFragment title = new TextFragment("Portfolio Title");
                title.Position = new Position(20, page.PageInfo.Height - 30); // 20 pts from left, 30 pts from top
                title.TextState.Font = FontRepository.FindFont("Helvetica");
                title.TextState.FontSize = 24;
                title.TextState.ForegroundColor = Aspose.Pdf.Color.White;

                page.Paragraphs.Add(title);
            }

            // Save the modified PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved portfolio with template to '{outputPath}'.");
    }
}
