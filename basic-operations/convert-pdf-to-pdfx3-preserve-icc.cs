using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Prepare conversion options for PDF/X‑3
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
            {
                // Preserve existing ICC profiles – leave IccProfileFileName null (default)
                // Log any conversion messages
                LogFileName = logPath,
                // Choose how to handle objects that cannot be converted
                ErrorAction = ConvertErrorAction.Delete
            };

            // Perform the conversion
            bool success = doc.Convert(options);
            if (!success)
            {
                Console.Error.WriteLine("Conversion reported errors. See log for details.");
            }

            // Save the resulting PDF/X‑3 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF converted to PDF/X‑3 and saved as '{outputPath}'.");
    }
}