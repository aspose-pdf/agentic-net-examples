using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Initialize the facade and bind the source PDF
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Create a stamp that will be placed as background on all pages
        Stamp stamp = new Stamp
        {
            IsBackground = true // place stamp behind page content
            // Additional properties can be set here, e.g. Opacity, SetOrigin, SetImageSize, etc.
        };

        // Add the stamp; Pages = null (default) applies it to every page
        fileStamp.AddStamp(stamp);

        // Save the stamped PDF
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}