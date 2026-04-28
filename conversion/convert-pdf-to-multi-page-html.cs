using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace (no Facades)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Base output HTML file name.
        // When SplitIntoPages is true, Aspose.Pdf will generate multiple files
        // (e.g., output.html, output_1.html, output_2.html, ...).
        const string outputHtmlPath = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Create HtmlSaveOptions and enable multi‑page output
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    SplitIntoPages = true
                    // Additional options can be set here if needed, e.g.:
                    // RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground,
                    // PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save the document as HTML using the configured options
                pdfDocument.Save(outputHtmlPath, htmlOptions);
            }

            Console.WriteLine("PDF successfully converted to multi‑page HTML.");
        }
        // HTML conversion relies on GDI+ and may fail on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}