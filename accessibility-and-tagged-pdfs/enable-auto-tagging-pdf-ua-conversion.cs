using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "tagged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options with automatic tagging enabled
            // Use the correct PDF/UA format constant
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1);

            // Enable auto‑tagging
            AutoTaggingSettings autoTagSettings = new AutoTaggingSettings
            {
                EnableAutoTagging = true
            };
            convOptions.AutoTaggingSettings = autoTagSettings;

            // Perform the conversion (auto‑tagging is applied during this step)
            doc.Convert(convOptions);

            // Save the resulting tagged PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
