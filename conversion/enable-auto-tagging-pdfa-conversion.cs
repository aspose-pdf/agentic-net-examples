using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally before conversion
        AutoTaggingSettings.Default.EnableAutoTagging = true;
        // Example: configure heading detection (optional)
        // AutoTaggingSettings.Default.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/A‑1B
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                OptimizeFileSize = true
                // PuaSymbolsProcessingStrategy can be set if needed
            };

            // Convert the document to PDF/A
            doc.Convert(convOptions);

            // Save the PDF/A output
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A saved to '{outputPath}'.");
    }
}