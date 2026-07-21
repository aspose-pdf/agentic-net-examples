using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions, including HtmlSaveOptions, are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "HtmlPages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Configure HTML save options to split each PDF page into a separate HTML file
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                SplitIntoPages = true,
                // Optional: embed raster images as PNG inside SVG wrappers
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // The file name provided is a template; Aspose.Pdf will generate
            // page_1.html, page_2.html, ... in the same folder.
            string htmlTemplatePath = Path.Combine(outputFolder, "page.html");

            try
            {
                pdfDoc.Save(htmlTemplatePath, htmlOptions);
                Console.WriteLine($"PDF pages have been converted to individual HTML files in '{outputFolder}'.");
            }
            catch (TypeInitializationException)
            {
                // HTML conversion relies on GDI+ which is Windows‑only
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
            }
            catch (DllNotFoundException)
            {
                // GDI+ DLL not found on non‑Windows platforms
                Console.WriteLine("GDI+ library not found. HTML conversion is unavailable on this platform.");
            }
        }
    }
}