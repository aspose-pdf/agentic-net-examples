using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Choose the first page (1‑based indexing)
                Page page = doc.Pages[1];

                // Define the annotation rectangle (fully qualified to avoid ambiguity)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

                // Create a HighlightAnnotation (a concrete TextMarkupAnnotation)
                HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
                {
                    // Set the highlight color
                    Color = Aspose.Pdf.Color.Yellow,
                    // Optional: add a comment that appears in the popup
                    Contents = "Important text highlighted",
                    // Optional: set the annotation title (shown in the popup title bar)
                    Title = "Reviewer"
                };

                // Add the annotation to the page
                page.Annotations.Add(highlight);

                // Save as HTML – must pass explicit HtmlSaveOptions
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Embed images as PNG inside SVG to keep output self‑contained
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion requires GDI+ (Windows only); handle possible platform limitation
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML file saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
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