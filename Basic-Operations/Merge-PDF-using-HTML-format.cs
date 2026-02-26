using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF files to be merged
        string[] pdfFiles = { "first.pdf", "second.pdf", "third.pdf" };
        // Output HTML file
        const string outputHtml = "merged.html";

        // Verify that at least one input file exists
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
            // Create the target document from the first PDF
            using (Document target = new Document(pdfFiles[0]))
            {
                // Merge remaining PDFs into the target document
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    if (!File.Exists(pdfFiles[i]))
                    {
                        Console.Error.WriteLine($"Skipping missing file: {pdfFiles[i]}");
                        continue;
                    }

                    using (Document source = new Document(pdfFiles[i]))
                    {
                        // Append all pages from the source document
                        target.Pages.Add(source.Pages);
                    }
                }

                // Prepare HTML save options (required for HTML output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources (images, CSS, fonts) into the single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Store raster images as PNGs wrapped in SVG (cross‑platform friendly)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion relies on GDI+ and is Windows‑only.
                // Wrap the save call in a try‑catch to handle non‑Windows platforms gracefully.
                try
                {
                    target.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"Merged PDF saved as HTML to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge or save: {ex.Message}");
        }
    }
}