using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure HTML save options with enhanced anti‑aliasing for smoother graphics
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Enable advanced anti‑aliasing for compound background images
                AntialiasingProcessing = HtmlSaveOptions.AntialiasingProcessingType.TryCorrectResultHtml
            };

            // Save the PDF as HTML using the custom options
            doc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"PDF converted to HTML with improved anti‑aliasing: {outputHtml}");
    }
}
