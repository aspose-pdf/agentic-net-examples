using System;
using System.IO;
using Aspose.Pdf;   // All SaveOptions (including HtmlSaveOptions) are in this namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // source PDF
        const string outputHtmlBase = "output_page.html"; // base name for generated HTML files

        // Verify that the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions
                {
                    // Enable per‑page HTML output
                    SplitIntoPages = true,

                    // Optional: embed images as PNGs inside SVG wrappers
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML (lifecycle: save)
                // When SplitIntoPages is true, Aspose.Pdf creates multiple files:
                // output_page.html, output_page_1.html, output_page_2.html, ...
                pdfDoc.Save(outputHtmlBase, htmlOptions);
            }

            Console.WriteLine("PDF successfully converted to per‑page HTML files.");
        }
        // HTML conversion relies on GDI+; on non‑Windows platforms it throws TypeInitializationException
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}