using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlLoadOptions (inherits from LoadOptions)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string intermediateHtmlPath = "intermediate.html";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // -------------------------------------------------
        // Step 1: Convert PDF -> HTML (single HTML file)
        // -------------------------------------------------
        try
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // HtmlSaveOptions defaults to a single HTML file (SplitIntoPages = false)
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Example: embed all resources into the HTML file
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    // Optional: choose raster image handling mode
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                pdfDoc.Save(intermediateHtmlPath, htmlOptions);
                Console.WriteLine($"PDF converted to HTML: {intermediateHtmlPath}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping this step.");
            return;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF→HTML conversion: {ex.Message}");
            return;
        }

        // -------------------------------------------------
        // Step 2: Convert HTML back to PDF
        // -------------------------------------------------
        if (!File.Exists(intermediateHtmlPath))
        {
            Console.Error.WriteLine($"Intermediate HTML not found: {intermediateHtmlPath}");
            return;
        }

        try
        {
            // Load the HTML file with HtmlLoadOptions
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();
            using (Document htmlDoc = new Document(intermediateHtmlPath, loadOptions))
            {
                htmlDoc.Save(outputPdfPath);
                Console.WriteLine($"HTML converted back to PDF: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during HTML→PDF conversion: {ex.Message}");
        }
    }
}