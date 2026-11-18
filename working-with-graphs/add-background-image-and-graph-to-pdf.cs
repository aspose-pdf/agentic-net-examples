using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string backgroundImgPath = "background.jpg";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(backgroundImgPath))
        {
            Console.Error.WriteLine($"Background image not found: {backgroundImgPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing is handled by the foreach)
            foreach (Page page in doc.Pages)
            {
                // ------------------------------------------------------------
                // Add the background image as a stamp placed behind page content
                // ------------------------------------------------------------
                ImageStamp bgStamp = new ImageStamp(backgroundImgPath)
                {
                    // Place the image behind existing content
                    Background = true,
                    // Scale the image to fill the entire page
                    Width = page.PageInfo.Width,
                    Height = page.PageInfo.Height
                };
                page.AddStamp(bgStamp);

                // ------------------------------------------------------------
                // Draw shapes on top of the background image using a Graph
                // ------------------------------------------------------------
                // Create a Graph that covers the whole page
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Example: a red rectangle with a light green fill
                float rectLeft = 100f;          // lower‑left X
                float rectBottom = 500f;        // lower‑left Y
                float rectWidth = 200f;         // width
                float rectHeight = 100f;        // height
                Aspose.Pdf.Drawing.Rectangle rectShape = new Aspose.Pdf.Drawing.Rectangle(rectLeft, rectBottom, rectWidth, rectHeight);
                rectShape.GraphInfo.FillColor = Color.FromRgb(0.8, 0.9, 0.5);
                rectShape.GraphInfo.Color = Color.Red;               // border colour
                rectShape.GraphInfo.LineWidth = 2f;                  // border thickness
                graph.Shapes.Add(rectShape);

                // Example: a blue ellipse
                float ellipseLeft = 350f;        // lower‑left X
                float ellipseBottom = 500f;      // lower‑left Y
                float ellipseWidth = 200f;       // width
                float ellipseHeight = 150f;      // height
                Aspose.Pdf.Drawing.Ellipse ellipseShape = new Aspose.Pdf.Drawing.Ellipse(ellipseLeft, ellipseBottom, ellipseWidth, ellipseHeight);
                ellipseShape.GraphInfo.FillColor = Color.FromRgb(0.6, 0.8, 1.0);
                ellipseShape.GraphInfo.Color = Color.Blue;
                ellipseShape.GraphInfo.LineWidth = 1.5f;
                graph.Shapes.Add(ellipseShape);

                // Add the graph (with its shapes) to the page
                page.Paragraphs.Add(graph);
            }

            // Save the modified PDF; the Document is still alive until Save completes
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with background image and overlay shapes saved to '{outputPdfPath}'.");
    }
}
