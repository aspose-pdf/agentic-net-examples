using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure SVG save options – enable CSS style embedding.
            // The RasterImagesSavingMode property does not exist on SvgSaveOptions.
            // Setting ScaleToPixels to true embeds CSS styles in the generated SVG.
            var svgOpts = new SvgSaveOptions
            {
                ScaleToPixels = true // enables CSS style embedding
            };

            // Save the PDF directly to an SVG file using the configured options.
            pdfDoc.Save(outputPath, svgOpts);
        }

        Console.WriteLine($"PDF successfully converted to SVG with embedded CSS: {outputPath}");
    }
}