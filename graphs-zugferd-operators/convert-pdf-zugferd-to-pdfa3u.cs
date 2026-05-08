using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF with ZUGFeRD attachment
        const string outputPath = "output_pdfa3u.pdf";   // Resulting PDF/A‑3U file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set up conversion options for PDF/A‑3U
            PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_3U);
            // (Optional) handle Private‑Use‑Area symbols if present
            // convertOptions.PuaSymbolsProcessingStrategy = 
            //     PdfFormatConversionOptions.PuaSymbolsProcessingStrategies.SurroundPuaTextWithEmptyActualText;

            // Perform the conversion; embedded files (ZUGFeRD XML) are preserved by default
            doc.Convert(convertOptions);

            // Save the converted document as PDF/A‑3U
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑3U file saved to '{outputPath}'.");
    }
}