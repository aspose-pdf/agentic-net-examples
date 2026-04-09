using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Output PDF/X‑3 file path
        const string outputPath = "output_pdfx3.pdf";

        // Optional log file to capture conversion messages
        const string logPath = "conversion_log.txt";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure conversion options for PDF/X‑3
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
            {
                // Write conversion messages to the log file
                LogFileName = logPath,

                // On conversion errors, delete the offending objects
                ErrorAction = ConvertErrorAction.Delete,

                // Preserve the existing ICC profile (null means “use the source profile”)
                IccProfileFileName = null
            };

            // Perform the conversion
            bool conversionResult = doc.Convert(options);

            // Inform the user about the conversion outcome
            Console.WriteLine(conversionResult
                ? "Conversion to PDF/X‑3 succeeded."
                : "Conversion completed with warnings; check the log file.");

            // Save the resulting PDF/X‑3 document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/X‑3 file saved to '{outputPath}'.");
    }
}
