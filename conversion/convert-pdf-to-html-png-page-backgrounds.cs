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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions();

            // Render page backgrounds as a single PNG per page
            htmlOptions.RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground;

            // Save as HTML using the configured options
            doc.Save(outputHtml, htmlOptions);
        }

        Console.WriteLine($"Conversion completed: {outputHtml}");
    }
}