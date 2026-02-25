using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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
            using (Document doc = new Document(inputPdf))
            {
                // Example: display page count
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Optional: extract text for demonstration
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text.Length}");

                // HTML conversion requires explicit options
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML saved to '{outputHtml}'.");
                }
                catch (TypeInitializationException)
                {
                    // GDI+ is Windows‑only; handle gracefully on other platforms
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