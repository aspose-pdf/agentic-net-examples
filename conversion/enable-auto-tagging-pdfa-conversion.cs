using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_pdfa.pdf";    // PDF/A result
        const string logPath    = "conversion_log.txt"; // conversion log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑1B format
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);

            // Assign auto‑tagging settings and enable the feature
            options.AutoTaggingSettings = AutoTaggingSettings.Default;
            options.AutoTaggingSettings.EnableAutoTagging = true;

            // Optional: configure heading recognition strategy, etc.
            // options.AutoTaggingSettings.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Specify a log file to capture conversion messages
            options.LogFileName = logPath;

            // Perform the conversion using the configured options
            bool conversionResult = doc.Convert(options);
            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion failed. See log for details.");
                return;
            }

            // Save the converted PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A document saved to '{outputPath}'.");
    }
}