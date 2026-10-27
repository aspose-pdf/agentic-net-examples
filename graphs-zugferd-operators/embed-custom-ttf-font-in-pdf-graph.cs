using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the custom TrueType font file
        const string fontPath = "MyCustomFont.ttf";
        // Output PDF file
        const string outputPdf = "graph_with_custom_font.pdf";

        if (!File.Exists(fontPath))
        {
            Console.Error.WriteLine($"Font file not found: {fontPath}");
            return;
        }

        // Load the custom font and ensure it will be embedded
        Font customFont = FontRepository.OpenFont(fontPath);
        customFont.IsEmbedded = true;

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // Add chart title using the custom font
            // -----------------------------------------------------------------
            TextFragment title = new TextFragment("Sample Bar Chart");
            title.TextState.Font = customFont;
            title.TextState.FontSize = 20;
            title.TextState.HorizontalAlignment = HorizontalAlignment.Center;
            page.Paragraphs.Add(title);

            // -----------------------------------------------------------------
            // Add X‑axis title using the custom font
            // -----------------------------------------------------------------
            TextFragment xAxisTitle = new TextFragment("Categories");
            xAxisTitle.TextState.Font = customFont;
            xAxisTitle.TextState.FontSize = 12;
            // Positioning will be handled later (simple flow layout)

            // -----------------------------------------------------------------
            // Add Y‑axis title (rotated) using the custom font
            // -----------------------------------------------------------------
            TextFragment yAxisTitle = new TextFragment("Values");
            yAxisTitle.TextState.Font = customFont;
            yAxisTitle.TextState.FontSize = 12;
            yAxisTitle.TextState.Rotation = 90;

            // -----------------------------------------------------------------
            // Create a simple bar chart using the Graph drawing container
            // -----------------------------------------------------------------
            const float graphWidth = 500f;
            const float graphHeight = 300f;
            Graph graph = new Graph(graphWidth, graphHeight);

            double[] data = new double[] { 10, 20, 30, 25, 15 };
            float barWidth = 40f;
            float barSpacing = 20f;
            float startX = 50f;
            float baseY = 20f;
            float yScale = (float)(graphHeight - 40) / 30f; // assuming max value ~30

            // Store bar labels to add after the graph
            List<TextFragment> barLabels = new List<TextFragment>();

            for (int i = 0; i < data.Length; i++)
            {
                float barHeight = (float)data[i] * yScale;
                float x1 = startX + i * (barWidth + barSpacing);
                float y1 = baseY;
                float x2 = x1 + barWidth;
                float y2 = baseY + barHeight;

                // Draw a filled rectangle to represent the bar
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(x1, y1, x2, y2)
                {
                    GraphInfo = new GraphInfo
                    {
                        Color = Color.Blue,
                        FillColor = Color.LightBlue,
                        LineWidth = 1
                    }
                };
                graph.Shapes.Add(rect);

                // Create label for the bar
                TextFragment barLabel = new TextFragment((i + 1).ToString());
                barLabel.TextState.Font = customFont;
                barLabel.TextState.FontSize = 10;
                barLabel.TextState.HorizontalAlignment = HorizontalAlignment.Center;
                // Position will be handled after the graph is placed
                barLabels.Add(barLabel);
            }

            // Add the graph drawing to the page
            page.Paragraphs.Add(graph);

            // Add axis titles after the graph (simple layout for demonstration)
            page.Paragraphs.Add(xAxisTitle);
            page.Paragraphs.Add(yAxisTitle);

            // Add bar labels after the axis titles
            foreach (var lbl in barLabels)
            {
                page.Paragraphs.Add(lbl);
            }

            // Save the PDF; the custom font will be embedded automatically
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with embedded custom TrueType font.");
    }
}
