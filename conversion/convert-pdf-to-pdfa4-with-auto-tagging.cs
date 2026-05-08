using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑4 format
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4);

            // Enable auto‑tagging using the default settings
            options.AutoTaggingSettings = AutoTaggingSettings.Default;
            options.AutoTaggingSettings.EnableAutoTagging = true;

            // Perform the conversion
            bool converted = doc.Convert(options);
            if (!converted)
            {
                Console.Error.WriteLine("Conversion to PDF/A‑4 failed.");
                return;
            }

            // Save the converted PDF/A‑4 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑4 file saved to '{outputPath}'.");
    }
}