using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPdfPath = "input.pdf";

        // Desired folder where SVG files will be written.
        const string outputFolderPath = "SvgOutput";

        // Verify that the input file exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the output folder exists.
        Directory.CreateDirectory(outputFolderPath);

        // The Save method expects a file name. When TreatTargetFileNameAsDirectory is true,
        // a folder with the same name as this file (minus extension) will be created and
        // each page will be saved as a separate SVG file inside it.
        string dummyOutputFile = Path.Combine(outputFolderPath, "output.svg");

        // Load the PDF document and convert it to SVG.
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Initialize SVG save options.
            SvgSaveOptions svgOptions = new SvgSaveOptions();

            // Create a directory for the SVG pages (named after the dummy file).
            svgOptions.TreatTargetFileNameAsDirectory = true;

            // Optional: enable multi‑threaded processing for faster conversion.
            svgOptions.IsMultiThreading = true;

            // Save the PDF as SVG files using the configured options.
            pdfDocument.Save(dummyOutputFile, svgOptions);
        }

        Console.WriteLine($"PDF has been converted to SVG files in folder: {outputFolderPath}");
    }
}