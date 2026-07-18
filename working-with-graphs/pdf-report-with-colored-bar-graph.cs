using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class PdfReportWithGraph
{
    static void Main()
    {
        const string outputPath = "ReportWithGraph.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (pages are 1‑based)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a Graph container that will hold the colored rectangles
            // Width = 400 points, Height = 200 points
            // ------------------------------------------------------------
            // NOTE: Graph constructor expects double values (obsolete float overload)
            Graph graph = new Graph(400.0, 200.0)
            {
                // Title expects a TextFragment, not a plain string
                Title = new TextFragment("Data Categories")
            };

            // Example data categories with colors and values (width of each bar)
            var categories = new[]
            {
                new { Name = "Category A", Color = Aspose.Pdf.Color.FromRgb(255.0/255.0,  99.0/255.0,   0.0/255.0), Width = 80.0f },
                new { Name = "Category B", Color = Aspose.Pdf.Color.FromRgb(  0.0/255.0, 180.0/255.0,   0.0/255.0), Width = 120.0f },
                new { Name = "Category C", Color = Aspose.Pdf.Color.FromRgb(  0.0/255.0,   0.0/255.0, 255.0/255.0), Width = 60.0f },
                new { Name = "Category D", Color = Aspose.Pdf.Color.FromRgb(255.0/255.0, 165.0/255.0,   0.0/255.0), Width = 140.0f }
            };

            // Layout parameters
            float barHeight = 30f;
            float barSpacing = 15f;
            float startX = 0f;   // left edge of the graph
            float startY = 0f;   // bottom edge of the graph

            // Add a rectangle shape for each category
            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];
                // Aspose.Pdf.Drawing.Rectangle(left, bottom, width, height)
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(
                    startX,
                    startY + i * (barHeight + barSpacing),
                    cat.Width,
                    barHeight);

                // Set visual properties via GraphInfo
                rect.GraphInfo = new GraphInfo
                {
                    FillColor = cat.Color,                     // fill with the category color
                    Color = Aspose.Pdf.Color.Black,            // border color
                    LineWidth = 1f                             // thin border
                };

                graph.Shapes.Add(rect);
            }

            // Add the graph to the page
            page.Paragraphs.Add(graph);

            // ------------------------------------------------------------
            // Create a legend using TextFragment objects placed below the graph
            // ------------------------------------------------------------
            float legendStartY = (float)graph.Height + 30f; // position legend below the graph
            for (int i = 0; i < categories.Length; i++)
            {
                var cat = categories[i];
                // Text fragment for the legend entry
                TextFragment legend = new TextFragment(cat.Name);
                // Position the text fragment (requires Aspose.Pdf.Text.Position)
                legend.Position = new Position(10, legendStartY + i * 20);
                // Optional: set font and color for the legend text
                legend.TextState.Font = FontRepository.FindFont("Helvetica");
                legend.TextState.FontSize = 12;
                legend.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(legend);
            }

            // Save the PDF document (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        // Fully qualify System.IO.Path to avoid ambiguity with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"PDF report generated: {System.IO.Path.GetFullPath(outputPath)}");
    }
}
