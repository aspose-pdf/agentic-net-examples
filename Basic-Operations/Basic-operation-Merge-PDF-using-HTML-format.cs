using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions (including HtmlSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        // Output HTML file
        const string outputHtml = "merged.html";

        // Verify that at least one source file exists
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files specified.");
            return;
        }

        // Ensure the first file exists before creating the target document
        if (!File.Exists(pdfFiles[0]))
        {
            Console.Error.WriteLine($"File not found: {pdfFiles[0]}");
            return;
        }

        try
        {
            // Load the first PDF as the base document (target)
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                // Append remaining PDFs
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    string srcPath = pdfFiles[i];
                    if (!File.Exists(srcPath))
                    {
                        Console.Error.WriteLine($"Skipping missing file: {srcPath}");
                        continue;
                    }

                    // Load source PDF in its own using block
                    using (Document srcDoc = new Document(srcPath))
                    {
                        // Merge pages from source into target
                        mergedDoc.Pages.Add(srcDoc.Pages);
                    }
                }

                // Prepare HTML save options (required to output HTML)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Example: embed all resources into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Example: embed raster images as PNGs inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ and works only on Windows.
                // Wrap the save call to handle cross‑platform scenarios gracefully.
                try
                {
                    mergedDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"Merged PDF saved as HTML → '{outputHtml}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge/save: {ex.Message}");
        }
    }
}