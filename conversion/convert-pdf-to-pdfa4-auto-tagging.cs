using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output-pdfa4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Configure conversion to PDF/A‑4 and enable auto‑tagging
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4)
            {
                AutoTaggingSettings = AutoTaggingSettings.Default
            };

            // Perform the conversion
            doc.Convert(options);

            // Save the resulting PDF/A‑4 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Converted to PDF/A‑4 with auto‑tagging: {outputPath}");
    }
}