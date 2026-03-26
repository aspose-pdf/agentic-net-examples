using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Configure auto‑tagging settings
            AutoTaggingSettings autoTagSettings = new AutoTaggingSettings();
            autoTagSettings.EnableAutoTagging = true;
            // Additional auto‑tagging options can be set here, e.g.:
            // autoTagSettings.HeadingRecognitionStrategy = HeadingRecognitionStrategy.Auto;

            // Create conversion options for PDF/A‑1B and assign the auto‑tagging settings
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            convOptions.AutoTaggingSettings = autoTagSettings;
            convOptions.OptimizeFileSize = true; // optional
            convOptions.LogFileName = logPath;

            // Perform the conversion
            doc.Convert(convOptions);

            // Save the resulting PDF/A document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}' with auto‑tagging enabled.");
    }
}