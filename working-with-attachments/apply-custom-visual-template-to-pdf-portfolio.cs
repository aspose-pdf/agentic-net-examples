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
        const string outputPath = "portfolio_templated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF Portfolio (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Apply a visual template to each page
            foreach (Page page in doc.Pages)
            {
                // Create a Graph that spans the entire page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // ---- Background rectangle (light gray) ----
                var background = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    0f,
                    (float)page.PageInfo.Width,
                    (float)page.PageInfo.Height);
                background.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.DarkGray,
                    LineWidth = 0f // No border
                };
                graph.Shapes.Add(background);

                // ---- Header banner rectangle (custom color) ----
                const float bannerHeight = 50f;
                var banner = new Aspose.Pdf.Drawing.Rectangle(
                    0f,
                    (float)(page.PageInfo.Height - bannerHeight),
                    (float)page.PageInfo.Width,
                    bannerHeight);
                banner.GraphInfo = new GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.FromRgb(0.2f, 0.4f, 0.8f), // Custom blue shade
                    Color = Aspose.Pdf.Color.White,
                    LineWidth = 0f
                };
                graph.Shapes.Add(banner);

                // ---- Text label on the banner ----
                TextFragment tf = new TextFragment("Custom Portfolio Template");
                tf.Position = new Position(20, page.PageInfo.Height - bannerHeight + 15);
                tf.TextState.FontSize = 18;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.White;
                page.Paragraphs.Add(tf);

                // Add the Graph (containing shapes) to the page
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF Portfolio (standard Save for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Portfolio saved with template to '{outputPath}'.");
    }
}
