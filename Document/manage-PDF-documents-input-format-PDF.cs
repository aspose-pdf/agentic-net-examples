using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output HTML file path
        const string outputHtmlPath = "output.html";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure HTML save options.
            // All numeric properties that expect a float use the 'f' suffix to avoid
            // implicit double‑to‑float conversion errors.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Embed all resources (fonts, images, CSS) directly into the HTML.
                PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,

                // Save raster images as PNGs embedded into SVG (requires a float value internally).
                // The enum does not need numeric literals, but if you ever set a numeric
                // property (e.g., image quality) use the 'f' suffix, e.g., 0.9f.
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the PDF as HTML using the explicit HtmlSaveOptions.
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"HTML file successfully created at: {outputHtmlPath}");
    }
}