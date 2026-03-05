using System;
using System.IO;
using Aspose.Pdf; // Core PDF API – HtmlSaveOptions lives directly in this namespace

class PdfToHtmlConverter
{
    static void Main()
    {
        const string inputPdf = "input.pdf";   // source PDF file
        const string outputHtml = "output.html"; // resulting HTML file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: create → load)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    // Render each PDF marked‑content layer as a separate HTML <div>
                    ConvertMarkedContentToLayers = true,

                    // Optional: embed images as PNG inside SVG (cross‑platform safe)
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,

                    // Optional: generate a single HTML file (set to false for multi‑page output)
                    SplitIntoPages = false
                };

                // Save the document as HTML (lifecycle: save)
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"PDF successfully converted to HTML: {outputHtml}");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
