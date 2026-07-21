using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ConvertErrorAction enum (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Create conversion options:
                // - Target format: PDF/A‑1b
                // - ConvertErrorAction.None tells the converter to skip elements it cannot convert
                //   (i.e., do not abort or delete them).
                PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(
                    logPath,                     // log file for conversion messages
                    PdfFormat.PDF_A_1B,          // target PDF/A‑1b format
                    ConvertErrorAction.None     // skip non‑convertible elements
                );

                // Perform the conversion
                bool success = doc.Convert(convOpts);
                Console.WriteLine($"Conversion succeeded: {success}");

                // Save the resulting PDF/A‑1b document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}