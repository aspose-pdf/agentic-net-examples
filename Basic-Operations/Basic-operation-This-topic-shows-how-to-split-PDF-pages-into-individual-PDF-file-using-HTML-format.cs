using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string pdfPath = "input.pdf";
        // Output HTML file name (base name; multiple files will be generated)
        const string htmlOutput = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,
                    // Embed raster images as PNG inside SVG (cross‑platform safe option)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    // Save the document; multiple HTML files will be created (one per page)
                    doc.Save(htmlOutput, htmlOpts);
                    Console.WriteLine("PDF successfully split into separate HTML pages.");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion relies on GDI+ and is Windows‑only
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}