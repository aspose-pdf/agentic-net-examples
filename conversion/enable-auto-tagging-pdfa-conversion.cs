using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath    = "conversion.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A output.
            // This constructor sets the log file, target PDF format and error handling action.
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                logPath,
                PdfFormat.PDF_A_1B,
                ConvertErrorAction.Delete);

            // Assign auto‑tagging settings. Use the default settings and ensure auto‑tagging is enabled.
            options.AutoTaggingSettings = AutoTaggingSettings.Default;
            options.AutoTaggingSettings.EnableAutoTagging = true;

            // Perform the conversion. The method returns true on success.
            bool converted = doc.Convert(options);
            if (!converted)
            {
                Console.Error.WriteLine("Conversion failed. See the log file for details.");
                return;
            }

            // Save the converted PDF/A document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A document saved to '{outputPath}'.");
    }
}