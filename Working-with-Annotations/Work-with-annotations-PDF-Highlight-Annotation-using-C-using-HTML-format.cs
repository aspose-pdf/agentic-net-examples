using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the rectangle (llx, lly, urx, ury) where the highlight will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a highlight annotation on the specified page and rectangle
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set a semi‑transparent yellow color for the highlight
                Color = Aspose.Pdf.Color.Yellow,
                Opacity = 0.5,
                // Optional comment that appears in the popup
                Contents = "Important text"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the document as HTML – must pass HtmlSaveOptions explicitly
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Embed all resources (images, CSS) directly into the HTML file
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                // Preserve image quality by embedding PNGs within SVG containers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            doc.Save(outputPath, htmlOpts);
        }

        Console.WriteLine($"PDF with highlight saved as HTML to '{outputPath}'.");
    }
}