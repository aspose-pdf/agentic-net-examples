using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "HtmlPages";

        // Verify source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle: create & load)
            using (Document doc = new Document(inputPdf))
            {
                // Configure HTML conversion to generate one HTML file per PDF page
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true          // enable per‑page HTML files
                    // SplitCssIntoPages = false;   // optional: keep a single CSS file
                };

                // Base file name for the generated HTML pages.
                // Aspose.Pdf will create files like "page_1.html", "page_2.html", …
                string baseHtmlPath = Path.Combine(outputDir, "page.html");

                // Save the document using the configured options (lifecycle: save)
                doc.Save(baseHtmlPath, htmlOpts);
            }

            Console.WriteLine("PDF successfully split into individual HTML pages.");
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}