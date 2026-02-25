using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf)) { Console.Error.WriteLine($"Not found: {inputPdf}"); return; }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Basic info
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Extract text using TextAbsorber
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Text characters: {absorber.Text.Length}");

                // Save as HTML with explicit options (required for non-PDF output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML → {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    // HTML conversion requires GDI+ (Windows only)
                    Console.WriteLine("HTML requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex) { Console.Error.WriteLine($"Error: {ex.Message}"); }
    }
}