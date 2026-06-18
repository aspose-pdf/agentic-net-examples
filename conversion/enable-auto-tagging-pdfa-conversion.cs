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

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑1B format
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);

            // Optional: write conversion messages to a log file
            options.LogFileName = logPath;

            // Enable automatic tagging during the conversion
            AutoTaggingSettings autoTagSettings = new AutoTaggingSettings
            {
                EnableAutoTagging = true
                // Additional settings can be configured here, e.g.:
                // HeadingRecognitionStrategy = HeadingRecognitionStrategy.Automatic,
                // HeadingLevels = new HeadingLevels()
            };
            options.AutoTaggingSettings = autoTagSettings;

            // Perform the conversion; the method returns true on success
            bool conversionSucceeded = doc.Convert(options);
            if (!conversionSucceeded)
            {
                Console.Error.WriteLine("Conversion failed. See the log file for details.");
                return;
            }

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A document saved to '{outputPath}'.");
    }
}