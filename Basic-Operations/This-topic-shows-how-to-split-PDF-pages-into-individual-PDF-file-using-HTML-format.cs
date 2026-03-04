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
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML save options to split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,          // one HTML file per PDF page
                    SplitCssIntoPages = false       // optional: keep a single CSS file
                };

                // Base file name for the generated HTML pages.
                // Aspose.Pdf will create files like "page.html", "page_1.html", "page_2.html", etc.
                string baseHtmlPath = Path.Combine(outputDir, "page.html");

                // Save using the configured options (no custom save logic)
                pdfDoc.Save(baseHtmlPath, htmlOpts);
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