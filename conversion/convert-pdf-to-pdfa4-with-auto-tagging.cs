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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Set up conversion options for PDF/A‑4
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_4);

            // Enable automatic tagging during conversion
            options.AutoTaggingSettings = new AutoTaggingSettings
            {
                EnableAutoTagging = true
            };

            // Convert the document using the specified options
            doc.Convert(options);

            // Save the converted PDF/A‑4 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑4 conversion completed: {outputPath}");
    }
}