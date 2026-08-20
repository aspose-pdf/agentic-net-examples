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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Set up conversion options for PDF/A‑4
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4);

                // Enable auto‑tagging using the default settings
                options.AutoTaggingSettings = AutoTaggingSettings.Default;
                options.AutoTaggingSettings.EnableAutoTagging = true;

                // Perform the conversion
                bool converted = doc.Convert(options);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion reported failure.");
                }

                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑4 saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}