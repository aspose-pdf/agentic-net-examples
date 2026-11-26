using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string htmlBase  = "output.html";   // base name for split HTML files

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Configure HTML conversion – split each PDF page into its own HTML file
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages        = true,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg,
                    PartsEmbeddingMode    = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml
                };

                // HTML conversion requires GDI+ (Windows only). Wrap in try‑catch to handle non‑Windows platforms.
                try
                {
                    doc.Save(htmlBase, htmlOpts);
                    Console.WriteLine($"PDF successfully converted to split HTML files starting with '{htmlBase}'.");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML conversion requires Windows (GDI+). Operation skipped on this platform.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}