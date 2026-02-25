using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.svg";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create SVG save options – required to force SVG output
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG using the options
                doc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}