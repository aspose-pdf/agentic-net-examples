using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "encrypted.pdf";   // encrypted PDF file
        const string outputHtml = "output.html";     // desired HTML output
        const string password   = "user123";        // user/owner password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the encrypted PDF using the password constructor.
        using (Document doc = new Document(inputPdf, password))
        {
            // Configure HTML conversion options (required for non‑PDF output).
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
            };

            // HTML conversion relies on GDI+ and is Windows‑only.
            try
            {
                doc.Save(outputHtml, htmlOpts);
                Console.WriteLine($"HTML saved to '{outputHtml}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}