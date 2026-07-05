using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf contains Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output HTML file path (base name; actual pages will be saved as input_1.html, input_2.html, ...)
        const string outputHtmlPath = "output.html";

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Configure HTML save options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                // Enable multi‑page output: each PDF page becomes a separate HTML file
                SplitIntoPages = true,

                // Optional: embed raster images as PNGs inside SVG wrappers (common choice)
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // Save the document as HTML using the configured options.
            // The Save method with a SaveOptions argument ensures the output format is HTML,
            // regardless of the file extension.
            pdfDocument.Save(outputHtmlPath, htmlOptions);
        }

        Console.WriteLine($"PDF successfully converted to multi‑page HTML at '{outputHtmlPath}'.");
    }
}