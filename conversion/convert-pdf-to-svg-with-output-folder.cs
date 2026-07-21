using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdfPath = "input.pdf";

        // Desired folder where SVG files will be placed.
        const string outputFolderPath = "output_svg";

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputFolderPath);

        // The file name used for the conversion.
        // With TreatTargetFileNameAsDirectory = true, a sub‑folder named "converted.svg"
        // will be created inside the output folder and will contain one SVG per page.
        string outputSvgPath = Path.Combine(outputFolderPath, "converted.svg");

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize SVG save options.
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Instruct the converter to create a directory (named after the output file)
            // and place each page's SVG inside it.
            svgOptions.TreatTargetFileNameAsDirectory = true;

            // Perform the conversion.
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG. Files are located in: {outputFolderPath}");
    }
}