using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_ua.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable automatic tagging globally
        AutoTaggingSettings.Default.EnableAutoTagging = true;

        // Set up conversion options for PDF/UA‑1 and attach the auto‑tagging settings
        PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1)
        {
            AutoTaggingSettings = AutoTaggingSettings.Default
        };

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/UA‑1 using the prepared options
                doc.Convert(conversionOptions);

                // Save the converted PDF/UA‑1 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted to PDF/UA‑1 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}