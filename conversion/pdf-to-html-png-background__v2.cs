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

        using (Document pdfDoc = new Document(inputPath))
        {
            HtmlSaveOptions options = new HtmlSaveOptions
            {
                // Render each page background as a single PNG image
                RasterImagesSavingMode = HtmlSaveOptions.RasterImagesSavingModes.AsEmbeddedPartsOfPngPageBackground
            };
            pdfDoc.Save(outputPath, options);
        }

        Console.WriteLine($"PDF converted to HTML: {outputPath}");
    }
}