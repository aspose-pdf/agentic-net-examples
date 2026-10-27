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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure SVG output options – enable CSS style embedding by scaling to pixels
            var svgOptions = new SvgSaveOptions
            {
                ScaleToPixels = true // embeds CSS styles into the generated SVG
            };

            // Save the PDF directly to SVG using the configured options
            pdfDoc.Save(outputPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG: {outputPath}");
    }
}
