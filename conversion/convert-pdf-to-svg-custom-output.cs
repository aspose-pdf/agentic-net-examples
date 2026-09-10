using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // SvgSaveOptions is also in this namespace

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Desired output folder for SVG files
        const string outputFolder = "SvgOutput";

        // Base name for the SVG files (first page will be output.svg, others output_2.svg, etc.)
        const string baseSvgName = "output.svg";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Combine folder and base file name to create the full output path
        string outputSvgPath = Path.Combine(outputFolder, baseSvgName);

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions();

                // Optional: treat the target file name as a directory.
                // When false (default), multiple pages are saved as:
                // output.svg, output_2.svg, output_3.svg, etc. in the same folder.
                // Set to true only if you want a dedicated folder named after the file.
                svgOptions.TreatTargetFileNameAsDirectory = false;

                // Save the PDF as SVG using the specified options
                pdfDocument.Save(outputSvgPath, svgOptions);
            }

            Console.WriteLine($"PDF successfully converted to SVG files in '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}