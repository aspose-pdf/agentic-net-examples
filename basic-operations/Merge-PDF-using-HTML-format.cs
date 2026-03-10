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

        // Verify that at least one source file exists
        if (pdfFiles.Length == 0)
        {
            Console.Error.WriteLine("No PDF files specified.");
            return;
        }

        foreach (string file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        try
        {
            // Load the first PDF as the base document
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                // Append remaining PDFs
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    using (Document src = new Document(pdfFiles[i]))
                    {
                        mergedDoc.Pages.Add(src.Pages);
                    }
                }

                // Prepare HTML save options (required for non‑PDF output)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Embed all resources into a single HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Save raster images as PNG wrapped in SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+; on non‑Windows platforms it may throw
                try
                {
                    mergedDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"Merged PDF saved as HTML: '{outputHtml}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during merge or save: {ex.Message}");
        }
    }
}