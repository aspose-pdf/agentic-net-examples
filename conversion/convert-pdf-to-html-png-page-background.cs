using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace contains Document and HtmlSaveOptions

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputHtml = "output.html";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure HTML conversion options
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                // Render each page background as a single embedded PNG image
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputHtml, htmlOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputHtml}'");
    }
}