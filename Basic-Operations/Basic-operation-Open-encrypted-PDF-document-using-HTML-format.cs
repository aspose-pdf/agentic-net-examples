using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath   = "encrypted.pdf";   // path to the encrypted PDF
        const string outputPath  = "output.html";     // desired HTML output file
        const string userPassword = "user123";        // user password for the PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF by providing the user password.
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Configure HTML conversion options explicitly.
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                // Optional: generate full HTML (default) or only body content.
                // HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteAllHtml
            };

            // HTML conversion relies on GDI+ and is Windows‑only.
            try
            {
                doc.Save(outputPath, htmlOptions);
                Console.WriteLine($"HTML saved to '{outputPath}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped.");
            }
        }
    }
}