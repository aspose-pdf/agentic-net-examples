using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string htmlFile = "intermediate.html";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Step 1: PDF → single HTML file
        try
        {
            using (Document pdfDoc = new Document(inputPdf))
            {
                HtmlSaveOptions htmlOpts = new HtmlSaveOptions
                {
                    SplitIntoPages = false,
                    PartsEmbeddingMode = HtmlSaveOptions.PartsEmbeddingModes.EmbedAllIntoHtml,
                    RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsPngImagesEmbeddedIntoSvg
                };
                pdfDoc.Save(htmlFile, htmlOpts);
            }
        }
        catch (TypeInitializationException)
        {
            Console.WriteLine("HTML conversion requires Windows (GDI+). Skipping.");
            return;
        }

        // Step 2: HTML → PDF
        using (Document htmlDoc = new Document(htmlFile, new HtmlLoadOptions()))
        {
            htmlDoc.Save(outputPdf);
        }

        Console.WriteLine($"Converted PDF → HTML → PDF saved as '{outputPdf}'.");
    }
}