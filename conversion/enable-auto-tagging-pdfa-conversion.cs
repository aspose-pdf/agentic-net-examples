using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Enable auto‑tagging globally
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Prepare conversion options for PDF/A output
        PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
        {
            // Assign the static AutoTaggingSettings instance to the options object
            AutoTaggingSettings = AutoTaggingSettings.Default
        };

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Perform the conversion using the prepared options
                doc.Convert(conversionOptions);

                // Save the resulting PDF/A document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}