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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert PDF to SVG while embedding CSS styles via pixel scaling.
                // The original PDF coordinate system (origin at bottom‑left) is preserved by default.
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    ScaleToPixels = true // enables CSS style embedding and keeps layout fidelity
                };

                doc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"SVG saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
