using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF, configure conversion options with auto‑tagging enabled,
        // convert to PDF/A, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Create conversion options for PDF/A‑1B format.
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);

            // Enable automatic tagging during conversion.
            convOptions.AutoTaggingSettings = new AutoTaggingSettings
            {
                EnableAutoTagging = true
            };

            // Perform the conversion. Returns true on success.
            bool success = doc.Convert(convOptions);
            if (!success)
            {
                Console.Error.WriteLine("Conversion failed.");
                return;
            }

            // Save the converted PDF/A document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}