using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "SvgOutput";

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
                // Configure SVG save options – treat the target file name as a directory
                // so each page is saved as a separate SVG file inside the folder.
                var svgOptions = new SvgSaveOptions
                {
                    TreatTargetFileNameAsDirectory = true,
                    // Enable CSS style embedding (required for proper SVG output)
                    ScaleToPixels = true
                };

                // The file name is ignored when TreatTargetFileNameAsDirectory is true;
                // only the directory part matters.
                string targetPath = Path.Combine(outputFolder, "page.svg");
                pdfDoc.Save(targetPath, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG files in folder: {outputFolder}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}