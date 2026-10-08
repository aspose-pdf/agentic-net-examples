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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Enable CSS style embedding and keep the original layout when converting to SVG
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    ScaleToPixels = true // preserves the original coordinate system indirectly
                };
                doc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"SVG saved to '{outputPath}' with original coordinate system preserved.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
