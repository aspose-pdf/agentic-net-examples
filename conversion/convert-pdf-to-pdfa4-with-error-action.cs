using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options:
            // - Target format: PDF/A‑4
            // - ErrorAction set to None to attempt conversion of problematic elements
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                PdfFormat.PDF_A_4,
                ConvertErrorAction.None);

            // Perform the conversion
            bool conversionResult = doc.Convert(options);
            Console.WriteLine($"Conversion succeeded: {conversionResult}");

            // Save the converted document (PDF/A‑4)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted PDF saved to '{outputPath}'.");
    }
}