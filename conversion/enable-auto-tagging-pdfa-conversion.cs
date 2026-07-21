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

        // Create conversion options for PDF/A (PDF/A‑1B) with error handling.
        PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

        // Enable automatic tagging during conversion.
        options.AutoTaggingSettings = new AutoTaggingSettings
        {
            EnableAutoTagging = true
            // Additional AutoTaggingSettings can be configured here if needed.
        };

        // Load the source PDF, convert it using the options, and save the PDF/A output.
        using (Document doc = new Document(inputPath))
        {
            bool converted = doc.Convert(options);
            if (!converted)
            {
                Console.Error.WriteLine("Conversion failed. Check the log for details.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}