using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html"; // base name; multiple pages will be generated

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options – split each PDF page into its own HTML file
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNG inside SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML with the specified options (extension alone is not enough)
                pdfDoc.Save(outputHtml, saveOptions);
                Console.WriteLine($"PDF successfully converted to split HTML pages (base: {outputHtml}).");
            }
        }
        // HTML conversion uses GDI+ and fails on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}