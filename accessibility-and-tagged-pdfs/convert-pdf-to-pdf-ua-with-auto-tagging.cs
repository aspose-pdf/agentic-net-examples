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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/UA (PDF/UA‑1) with automatic tagging
            var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_UA_1)
            {
                // Enable the default auto‑tagging settings
                AutoTaggingSettings = AutoTaggingSettings.Default
            };

            // Perform the conversion; this applies the auto‑tagging process
            doc.Convert(convOptions);

            // Save the resulting PDF as a tagged PDF (PDF/UA compliant)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Tagged PDF saved to '{outputPath}'.");
    }
}
