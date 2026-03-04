using System;
using System.IO;
using Aspose.Pdf;               // Core API
using Aspose.Pdf;               // HtmlSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input PDF files to be merged before splitting.
        string[] pdfFiles = { "input1.pdf", "input2.pdf", "input3.pdf" };
        // Output HTML file (will become a set of HTML pages because SplitIntoPages = true).
        string outputHtml = "merged.html";

        // Verify that at least the first file exists.
        if (!File.Exists(pdfFiles[0]))
        {
            Console.Error.WriteLine($"Not found: {pdfFiles[0]}");
            return;
        }

        try
        {
            // Load the first PDF as the base document.
            using (Document mergedDoc = new Document(pdfFiles[0]))
            {
                // Append remaining PDFs (if any) to the base document.
                for (int i = 1; i < pdfFiles.Length; i++)
                {
                    if (!File.Exists(pdfFiles[i]))
                    {
                        Console.Error.WriteLine($"Skipping: {pdfFiles[i]}");
                        continue;
                    }

                    using (Document src = new Document(pdfFiles[i]))
                    {
                        mergedDoc.Pages.Add(src.Pages);
                    }
                }

                // Configure HTML save options to split each PDF page into a separate HTML file.
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true, // Enable per‑page HTML output.
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the merged PDF as HTML. On non‑Windows platforms this may throw
                // TypeInitializationException because HTML conversion relies on GDI+.
                try
                {
                    mergedDoc.Save(outputHtml, htmlOptions);
                    Console.WriteLine($"HTML conversion completed: '{outputHtml}'");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}