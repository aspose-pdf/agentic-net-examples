using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML save options to split each PDF page into its own HTML file
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                SplitIntoPages = true,
                // Optional: embed raster images as PNGs inside SVG for better compatibility
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch for cross‑platform safety.
            try
            {
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF split into HTML pages saved (base name: {outputHtml}).");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
            }
        }
    }
}