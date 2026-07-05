using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑4.
            // Set ConvertErrorAction to None to attempt conversion of problematic elements.
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4, ConvertErrorAction.None);
            options.LogFileName = logPath; // where conversion messages will be written

            // Perform the conversion
            bool success = doc.Convert(options);
            Console.WriteLine($"Conversion succeeded: {success}");

            // Save the converted PDF/A‑4 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑4 saved to '{outputPath}'. Log written to '{logPath}'.");
    }
}