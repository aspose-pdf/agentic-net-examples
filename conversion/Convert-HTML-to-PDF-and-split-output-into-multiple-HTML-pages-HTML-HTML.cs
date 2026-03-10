using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace (contains Document, HtmlSaveOptions, HtmlLoadOptions)

class Program
{
    static void Main()
    {
        const string htmlInputPath  = "input.html";          // Source HTML file
        const string htmlOutputPath = "output.html";         // Base name for split HTML output

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {htmlInputPath}");
            return;
        }

        try
        {
            // Load the HTML document. HtmlLoadOptions tells Aspose.Pdf that the source format is HTML.
            using (Document doc = new Document(htmlInputPath, new HtmlLoadOptions()))
            {
                // Configure HTML save options to split each PDF page into a separate HTML file.
                HtmlSaveOptions saveOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                                 // Enable multi‑page output
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save the document as HTML. With SplitIntoPages = true, Aspose.Pdf will generate
                // multiple files (output.html, output_1.html, output_2.html, ...).
                doc.Save(htmlOutputPath, saveOptions);
            }

            Console.WriteLine($"HTML conversion completed. Split files start with '{htmlOutputPath}'.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is only supported on Windows.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}