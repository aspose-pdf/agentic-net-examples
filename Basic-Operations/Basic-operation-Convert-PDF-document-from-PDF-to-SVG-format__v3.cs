using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.svg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Initialize SVG save options (default constructor)
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Save the document as SVG, passing the explicit SaveOptions
                pdfDoc.Save(outputPath, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}