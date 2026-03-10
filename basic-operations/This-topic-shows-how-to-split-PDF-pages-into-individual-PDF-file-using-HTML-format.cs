using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "HtmlPages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into its own HTML file.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Optional: embed raster images as PNG inside SVG for better portability.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // The base file name; Aspose.Pdf will append page numbers (e.g., page_1.html, page_2.html, ...).
                string baseHtmlPath = Path.Combine(outputDir, "page.html");

                // Save the PDF as multiple HTML pages.
                pdfDoc.Save(baseHtmlPath, htmlOpts);
            }

            Console.WriteLine("PDF successfully split into individual HTML pages.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}