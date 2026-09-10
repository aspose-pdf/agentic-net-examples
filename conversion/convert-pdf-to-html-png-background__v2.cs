using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Initialize HTML save options
                HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

                // Render each page background as a single PNG image (embedded per page)
                htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

                // Save the document as HTML using explicit save options (required for non‑PDF output)
                pdfDoc.Save(outputHtml, htmlOptions);
            }

            Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
        }
        // HTML conversion relies on GDI+ and may fail on non‑Windows platforms
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