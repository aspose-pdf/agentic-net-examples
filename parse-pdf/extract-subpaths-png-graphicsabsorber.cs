using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a self‑contained sample PDF that contains vector graphics.
        // ---------------------------------------------------------------------
        const string samplePdfPath = "sample.pdf";
        CreateSamplePdf(samplePdfPath);

        // ---------------------------------------------------------------------
        // 2. Directory where PNG images of each sub‑path will be saved.
        // ---------------------------------------------------------------------
        const string outputDirectory = "SubpathsPng";
        Directory.CreateDirectory(outputDirectory);

        // ---------------------------------------------------------------------
        // 3. Load the PDF that we just created and extract its vector graphics.
        // ---------------------------------------------------------------------
        using (Document pdfDoc = new Document(samplePdfPath))
        {
            // Process the first page – adapt the loop if you need all pages.
            Page page = pdfDoc.Pages[1];

            // Capture all graphic elements on the page.
            GraphicsAbsorber graphicsAbsorber = new GraphicsAbsorber();
            graphicsAbsorber.Visit(page);

            int subPathIndex = 1;

            foreach (GraphicElement element in graphicsAbsorber.Elements)
            {
                // -------------------------------------------------------------
                // 4. Convert the captured graphic element to SVG markup.
                // -------------------------------------------------------------
                string svgContent = element.SaveToSvg();
                string tempSvgPath = System.IO.Path.Combine(outputDirectory, $"subpath_{subPathIndex}.svg");
                File.WriteAllText(tempSvgPath, svgContent);

                // -------------------------------------------------------------
                // 5. Load the SVG as a PDF document (Aspose.Pdf can render SVG).
                // -------------------------------------------------------------
                using (Document svgDoc = new Document(tempSvgPath, new SvgLoadOptions()))
                {
                    // Convert the first (and only) page of the SVG document to PNG.
                    using (MemoryStream pngStream = svgDoc.Pages[1].ConvertToPNGMemoryStream())
                    {
                        string pngPath = System.IO.Path.Combine(outputDirectory, $"subpath_{subPathIndex}.png");
                        File.WriteAllBytes(pngPath, pngStream.ToArray());
                    }
                }

                // Clean up the temporary SVG file.
                File.Delete(tempSvgPath);
                subPathIndex++;
            }
        }

        Console.WriteLine($"Extraction complete. PNG images are saved in the '{outputDirectory}' folder.");
    }

    /// <summary>
    /// Generates a simple PDF that contains a few vector shapes.
    /// The method is deliberately self‑contained so the example does not rely on external files.
    /// </summary>
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add a page.
            Page page = doc.Pages.Add();

            // Create a Graph (container for vector shapes) with a defined size.
            Graph graph = new Graph(500.0, 300.0);

            // 1) A diagonal line.
            Line line = new Line(new float[] { 0, 0, 500, 300 });
            line.GraphInfo = new GraphInfo { Color = Color.Blue, LineWidth = 2 };
            graph.Shapes.Add(line);

            // 2) A rectangle.
            Aspose.Pdf.Drawing.Rectangle rect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 150);
            rect.GraphInfo = new GraphInfo
            {
                Color = Color.Green,
                FillColor = Color.FromArgb(128, 0, 255, 0), // 50 % transparent green fill
                LineWidth = 2
            };
            graph.Shapes.Add(rect);

            // 3) A cubic Bezier curve.
            Curve curve = new Curve(new float[] { 250, 250, 300, 100, 400, 400, 450, 150 });
            curve.GraphInfo = new GraphInfo { Color = Color.Red, LineWidth = 2 };
            graph.Shapes.Add(curve);

            // Add the graph to the page.
            page.Paragraphs.Add(graph);

            // Save the PDF.
            doc.Save(path);
        }
    }
}
