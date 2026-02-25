using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlPath = "output.html"; // base name; multiple files will be created

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNG inside SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document; multiple HTML files will be generated (output.html, output_1.html, …)
                pdfDoc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine($"PDF split into HTML pages successfully. Files start with '{outputHtmlPath}'.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is only supported on Windows
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}