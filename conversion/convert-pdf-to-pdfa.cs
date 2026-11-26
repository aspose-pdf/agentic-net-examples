using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion to PDF/A‑1B
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                // Optional: enable file size optimization
                OptimizeFileSize = true
                // No PuaSymbolsProcessingStrategy needed unless specific handling is required
            };

            // Perform the conversion
            doc.Convert(conversionOptions);

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}