using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileStamp, FormattedText

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a PdfFileStamp instance (do NOT wrap in using – it does not implement IDisposable)
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the source PDF file to the stamp processor
        fileStamp.BindPdf(inputPath);

        // Create a FormattedText object containing the placeholder for the file name.
        // The placeholder {file_name} will be replaced with the actual file name when the stamp is applied.
        FormattedText headerText = new FormattedText("{file_name}");

        // Add the header to all pages. The second argument is the top margin (in points).
        fileStamp.AddHeader(headerText, 20f);

        // Save the stamped PDF to the output path
        fileStamp.Save(outputPath);

        // Close the facade to release resources
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}