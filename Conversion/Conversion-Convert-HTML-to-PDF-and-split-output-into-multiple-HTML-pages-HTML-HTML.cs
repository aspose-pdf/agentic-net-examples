using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, HtmlLoadOptions, HtmlSaveOptions)

class Program
{
    static void Main()
    {
        const string inputHtmlPath  = "input.html";          // source HTML file
        const string outputHtmlBase = "output.html";         // base name for split HTML pages

        if (!File.Exists(inputHtmlPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputHtmlPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions is optional but makes the intent explicit.
            using (Document doc = new Document(inputHtmlPath, new HtmlLoadOptions()))
            {
                // Configure HTML save options to split each PDF page (here each source page) into a separate HTML file.
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                         // enable multi‑page HTML output
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML. With SplitIntoPages=true Aspose.Pdf creates multiple files:
                // output.html (first page), output_1.html, output_2.html, ... etc.
                doc.Save(outputHtmlBase, saveOptions);
            }

            Console.WriteLine($"HTML conversion completed. Split files start with '{outputHtmlBase}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML‑to‑HTML conversion uses GDI+ internally; on non‑Windows platforms this may fail.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}