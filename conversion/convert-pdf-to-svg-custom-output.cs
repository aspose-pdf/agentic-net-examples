using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdfPath = "input.pdf";

        // Desired output folder for the SVG files.
        const string outputFolderPath = "output_svg";

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolderPath);

        // Full path for the primary SVG output file.
        // If the PDF has multiple pages, additional files will be created
        // with suffixes (_2.svg, _3.svg, etc.) in the same folder.
        string outputSvgPath = Path.Combine(outputFolderPath, "output.svg");

        // Verify that the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and save it as SVG using core API.
        using (Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(inputPdfPath))
        {
            // Initialize SVG save options.
            Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();

            // Optional: treat the target file name as a directory so that each page
            // is saved as a separate SVG file inside the folder.
            // svgOptions.TreatTargetFileNameAsDirectory = true;

            // Save the PDF as SVG.
            pdfDocument.Save(outputSvgPath, svgOptions);
        }

        Console.WriteLine($"PDF successfully converted to SVG at: {outputSvgPath}");
    }
}