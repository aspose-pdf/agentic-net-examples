using System;
using System.IO;
using Aspose.Pdf;               // Core API and all SaveOptions are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputHtmlDir = "output_html";   // directory where split pages will be placed

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputHtmlDir);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML save options – enable multi‑page output
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                         // each PDF page → separate HTML file
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    // Optional: embed all resources into HTML files
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save each page as a separate HTML file.
                // The library will generate files named like "output_html_page_1.html", etc.
                // Wrap in try‑catch because HTML conversion requires GDI+ (Windows only).
                try
                {
                    // The output path can be a folder; the library will create files inside it.
                    // Provide a dummy file name; actual files are created per page.
                    string dummyOutputPath = Path.Combine(outputHtmlDir, "index.html");
                    pdfDoc.Save(dummyOutputPath, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to multi‑page HTML in folder: {outputHtmlDir}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}