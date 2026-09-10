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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Initialize PdfFileStamp with the loaded document.
            PdfFileStamp fileStamp = new PdfFileStamp(doc);

            // Use Arabic numerals and ensure leading zeros via the format string.
            fileStamp.NumberingStyle = NumberingStyle.NumeralsArabic;
            // The '#' placeholder will be replaced by the page number.
            // Prefix with zeros to get leading zeros (e.g., 001, 002, ...).
            fileStamp.AddPageNumber("00#");

            // Save the stamped PDF.
            fileStamp.Save(outputPath);

            // Close the facade (required to release internal resources).
            fileStamp.Close();
        }

        Console.WriteLine($"Page-number stamped PDF saved to '{outputPath}'.");
    }
}