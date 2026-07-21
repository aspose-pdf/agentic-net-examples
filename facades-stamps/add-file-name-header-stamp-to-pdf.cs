using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for EncodingType

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get the file name to use in the header
        string fileName = Path.GetFileName(inputPath);

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.InputFile = inputPath;
        fileStamp.OutputFile = outputPath;

        // Create formatted text for the header (using System.Drawing.Color as required by FormattedText)
        FormattedText header = new FormattedText(
            fileName,
            System.Drawing.Color.Black,
            "Helvetica",
            EncodingType.Winansi,
            false,
            12);

        // Add the header with a top margin (e.g., 20 points)
        fileStamp.AddHeader(header, 20);

        // Save the result
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}