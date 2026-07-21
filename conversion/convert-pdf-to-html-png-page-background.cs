using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Set HTML save options to embed all background images as a single PNG per page
            HtmlSaveOptions htmlOptions = new HtmlSaveOptions
            {
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };

            // Save the document as HTML using the configured options
            pdfDoc.Save(outputPath, htmlOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}