using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Work with the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // ----- Add a square shape annotation -----
                // Rectangle: llx, lly, urx, ury
                var squareRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);
                var squareAnno = new SquareAnnotation(page, squareRect);
                squareAnno.Color = Aspose.Pdf.Color.Red;          // border color
                squareAnno.Contents = "Square Annotation";
                page.Annotations.Add(squareAnno);

                // ----- Add a stamp (figure) annotation -----
                var stampRect = new Aspose.Pdf.Rectangle(250, 500, 350, 600);
                var stampAnno = new StampAnnotation(page, stampRect);
                stampAnno.Color = Aspose.Pdf.Color.Blue;
                stampAnno.Contents = "Figure Annotation";
                page.Annotations.Add(stampAnno);

                // ----- Save as HTML (explicit HtmlSaveOptions) -----
                var htmlOpts = new HtmlSaveOptions
                {
                    // Embed all resources into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Render images as PNG embedded into SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only)
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}