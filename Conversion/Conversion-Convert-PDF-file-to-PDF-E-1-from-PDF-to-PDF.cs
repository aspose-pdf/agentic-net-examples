using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfe1.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF and convert it to PDF/E‑1 format.
        // The conversion logs any issues to the specified log file.
        using (Document doc = new Document(inputPath))
        {
            // Convert to PDF/E‑1.  Use Delete to remove objects that cannot be converted.
            // Adjust the enum value if your Aspose.Pdf version uses a different name for PDF/E‑1.
            doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
    }
}