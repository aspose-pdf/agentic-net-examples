using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputHtml = "output.html";       // base name for HTML output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion to split each PDF page into a separate HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true, // one HTML file per PDF page
                    // Optional: embed images as PNGs inside the HTML files
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save the document as HTML with the specified options (lifecycle: save)
                // On non‑Windows platforms this may throw TypeInitializationException because GDI+ is required.
                try
                {
                    pdfDoc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"PDF split into HTML pages successfully. Base file: {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}