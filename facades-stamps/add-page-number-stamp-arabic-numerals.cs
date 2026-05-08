using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileStamp facade
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Load the source PDF
        fileStamp.BindPdf(inputPath);

        // Use Arabic numerals (default) and start numbering at 1
        fileStamp.NumberingStyle = NumberingStyle.NumeralsArabic;
        fileStamp.StartingNumber = 1;

        // Add page numbers. The placeholder '#' is replaced with the page number.
        // Leading zeros are not directly supported by PdfFileStamp; this adds plain numbers.
        fileStamp.AddPageNumber("Page #");

        // Save the stamped PDF
        fileStamp.Save(outputPath);

        // Release resources
        fileStamp.Close();

        Console.WriteLine($"Page numbers added to '{outputPath}'.");
    }
}