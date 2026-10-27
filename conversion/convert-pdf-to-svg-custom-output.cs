using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdfPath = "input.pdf";

        // Desired output folder for the SVG file
        const string outputFolder = "SvgOutput";

        // Combine folder and file name for the final SVG path
        string outputSvgPath = Path.Combine(outputFolder, "output.svg");

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure SVG save options (all options are in Aspose.Pdf namespace)
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    // Example option: save each page as a separate SVG file inside the folder
                    // Uncomment the following line if per‑page SVGs are desired
                    // PageSavingMode = SvgSaveOptions.PageSavingModes.SinglePage
                };

                // Save the PDF as SVG using the specified options
                pdfDoc.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG: {outputSvgPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}