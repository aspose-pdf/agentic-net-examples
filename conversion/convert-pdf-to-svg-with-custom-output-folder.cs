using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Custom output folder where SVG files will be written
        const string outputFolderPath = "output_svg";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolderPath);

        // Load the PDF document (lifecycle: load)
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // Save each page as an individual SVG file into the output folder.
            // When a folder path is supplied, Aspose.Pdf creates one SVG per page.
            pdfDocument.Save(outputFolderPath, SaveFormat.Svg);
        }

        Console.WriteLine($"PDF has been converted to SVG files in folder: {outputFolderPath}");
    }
}
