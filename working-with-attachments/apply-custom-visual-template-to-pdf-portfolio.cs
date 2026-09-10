using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class ApplyTemplateToPortfolio
{
    static void Main()
    {
        // Input PDF Portfolio file path
        const string inputPath = "portfolio_input.pdf";
        // Output PDF file with visual template applied
        const string outputPath = "portfolio_with_template.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF Portfolio
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages and apply a visual template
            foreach (Page page in doc.Pages)
            {
                // Create a Graph container sized to the page dimensions (double values)
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Add a semi‑transparent rectangle as a background overlay
                // Use the drawing‑specific Rectangle type (expects float parameters)
                var backgroundRect = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.PageInfo.Width,
                    (float)page.PageInfo.Height);
                backgroundRect.GraphInfo = new GraphInfo
                {
                    // Light gray fill with 50% opacity
                    FillColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9),
                    // No border
                    Color = Aspose.Pdf.Color.Transparent,
                    LineWidth = 0f
                };
                graph.Shapes.Add(backgroundRect);

                // Optionally add a header text using TextFragment
                TextFragment header = new TextFragment("Custom Visual Template");
                // Configure appearance via the existing TextState (read‑only property)
                header.TextState.Font = FontRepository.FindFont("Helvetica");
                header.TextState.FontSize = 24;
                header.TextState.ForegroundColor = Aspose.Pdf.Color.DarkBlue;
                // Position the text near the top of the page
                header.Position = new Position(50, page.PageInfo.Height - 50);
                // Add the text fragment to the page
                page.Paragraphs.Add(header);

                // Add the Graph (with the rectangle) to the page
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"Template applied and saved to '{outputPath}'.");
    }
}