using System;
using System.IO;
using Aspose.Pdf;               // Document, HtmlSaveOptions, etc.

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputHtml = "output.html";       // base name for HTML files

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure HTML conversion to generate one HTML file per PDF page.
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = true,                     // enable multi‑page output
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // Save – Aspose.Pdf will create output.html, output_1.html, output_2.html, …
                pdfDoc.Save(outputHtml, htmlOpts);
                Console.WriteLine("PDF successfully split into separate HTML pages.");
            }
        }
        catch (TypeInitializationException)
        {
            // HTML conversion relies on GDI+ and is Windows‑only.
            Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}