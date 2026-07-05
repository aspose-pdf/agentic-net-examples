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
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Create conversion options for PDF/A‑4 with auto‑tagging enabled
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4, ConvertErrorAction.Delete);
                // Use the default auto‑tagging settings (auto‑tagging is enabled by default)
                options.AutoTaggingSettings = AutoTaggingSettings.Default;
                // Explicitly ensure auto‑tagging is turned on
                options.AutoTaggingSettings.EnableAutoTagging = true;

                // Convert the document to PDF/A‑4
                bool converted = doc.Convert(options);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/A‑4 failed.");
                }

                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑4 file saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}