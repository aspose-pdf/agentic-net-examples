using System;
using System.IO;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Get the first page (1‑based indexing)
                Aspose.Pdf.Page page = doc.Pages[1];

                // Create a graph container to hold vector shapes
                Aspose.Pdf.Drawing.Graph graph = new Aspose.Pdf.Drawing.Graph(500, 300);

                // ----- Rectangle -----
                Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 200, 200, 100);
                rect.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.LightGray,
                    Color = Aspose.Pdf.Color.Black,
                    LineWidth = 2
                };
                graph.Shapes.Add(rect);

                // ----- Ellipse -----
                Aspose.Pdf.Drawing.Ellipse ellipse = new Aspose.Pdf.Drawing.Ellipse(300, 200, 150, 100);
                ellipse.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    FillColor = Aspose.Pdf.Color.Yellow,
                    Color = Aspose.Pdf.Color.Red,
                    LineWidth = 1.5f
                };
                graph.Shapes.Add(ellipse);

                // ----- Line -----
                float[] linePoints = { 100, 100, 400, 250 };
                Aspose.Pdf.Drawing.Line line = new Aspose.Pdf.Drawing.Line(linePoints);
                line.GraphInfo = new Aspose.Pdf.GraphInfo
                {
                    Color = Aspose.Pdf.Color.Blue,
                    LineWidth = 3
                };
                graph.Shapes.Add(line);

                // Add the graph (with all shapes) to the page
                page.Paragraphs.Add(graph);

                // Configure HTML save options
                Aspose.Pdf.HtmlSaveOptions htmlOptions = new Aspose.Pdf.HtmlSaveOptions
                {
                    // Embed raster images as Base64‑encoded PNGs inside SVG wrappers
                    RasterImagesSavingMode = Aspose.Pdf.HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Embed all resources directly into the HTML file
                    PartsEmbeddingMode = Aspose.Pdf.HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Generate full HTML markup (default)
                    HtmlMarkupGenerationMode = Aspose.Pdf.HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
                };

                // Save the modified document as HTML
                doc.Save(outputHtml, htmlOptions);
            }

            Console.WriteLine($"HTML saved to '{outputHtml}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}