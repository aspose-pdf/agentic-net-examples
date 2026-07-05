using System;
using System.IO;
using Aspose.Pdf;
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

        // Initialize the stamp facade and bind the source PDF (use the non‑obsolete constructor)
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPath);

        // Use Arabic numerals for page numbering (enum lives in Aspose.Pdf namespace)
        fileStamp.NumberingStyle = NumberingStyle.NumeralsArabic;

        // Starting number (default is 1, can be changed if needed)
        fileStamp.StartingNumber = 1;

        // Add page numbers with leading zeros (e.g., 001, 002, …)
        fileStamp.AddPageNumber("00#");

        // Save the stamped PDF to the desired output path
        fileStamp.Save(outputPath);
        fileStamp.Close();

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
