using System;
using System.IO;
using Aspose.Pdf; // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string htmlPath = "output.html";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Open the PDF from a FileStream inside a using block for deterministic disposal
            using (FileStream pdfStream = File.OpenRead(pdfPath))
            using (Document doc = new Document(pdfStream)) // Load PDF from stream
            {
                // Configure HTML conversion options (required for non‑PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // HTML conversion uses GDI+ and is Windows‑only; handle possible platform exceptions
                try
                {
                    doc.Save(htmlPath, htmlOpts); // Explicit SaveOptions ensures HTML output
                    Console.WriteLine($"HTML saved to '{htmlPath}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}