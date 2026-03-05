using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputHtmlPath  = "input.html";   // source HTML file
        const string outputHtmlPath = "output.html";  // base name for generated HTML pages

        // Verify input file exists
        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        try
        {
            // Load the HTML document (HTML → PDF internal representation)
            using (Document doc = new Document(inputHtmlPath, new HtmlLoadOptions()))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true, // enable multi‑page output
                    // Optional: embed all resources into each HTML page
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML; multiple files will be created (output.html, output_1.html, output_2.html, …)
                doc.Save(outputHtmlPath, htmlOpts);
                Console.WriteLine("HTML conversion completed and output split into multiple pages.");
            }
        }
        // HTML conversion relies on GDI+ and is Windows‑only
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}