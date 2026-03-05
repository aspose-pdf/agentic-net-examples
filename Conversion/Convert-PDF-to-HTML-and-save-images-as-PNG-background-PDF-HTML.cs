using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF – Document implements IDisposable, so wrap in using
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions();

                // Save each page as a single PNG background image
                htmlOpts.RasterImagesSavingMode = Aspose.Pdf.HtmlSaveOptions.RasterImagesSavingModes
                    .AsEmbeddedPartsOfPngPageBackground;

                // Optional: embed all CSS, fonts, etc., into the HTML file
                // htmlOpts.PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml;

                // Save as HTML using the explicit SaveOptions (required for non‑PDF output)
                pdfDoc.Save(outputHtml, htmlOpts);
            }

            Console.WriteLine($"HTML saved to '{outputHtml}'.");
        }
        // HTML conversion uses GDI+ and throws on non‑Windows platforms
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows GDI+. Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}