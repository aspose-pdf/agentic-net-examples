using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF file and desired HTML output file
        const string inputPdf   = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Basic information about the PDF
                Console.WriteLine($"Pages: {doc.Pages.Count}");

                // Extract text using TextAbsorber (demonstrates a basic operation)
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Text characters: {absorber.Text.Length}");

                // Configure HTML conversion options explicitly (required for HTML output)
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    PartsEmbeddingMode     = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };

                // Save as HTML; wrap in try‑catch because HTML conversion needs GDI+ (Windows only)
                try
                {
                    doc.Save(outputHtml, htmlOpts);
                    Console.WriteLine($"HTML → {outputHtml}");
                }
                catch (TypeInitializationException)
                {
                    Console.WriteLine("HTML requires Windows (GDI+). Skipped.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}