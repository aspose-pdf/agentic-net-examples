using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PdfFormat enum if needed (it's in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
        const string logPath    = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(inputPath))
            {
                // Create conversion options for PDF/A‑3b.
                // The ConvertErrorAction enum only defines Delete and None.
                // Since a "Throw" option does not exist, we use None and
                // manually raise an exception if the conversion reports failure.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    logPath,                     // log file for conversion messages
                    PdfFormat.PDF_A_3B,          // target PDF/A‑3b format
                    ConvertErrorAction.None      // no automatic deletion of problematic objects
                );

                // Perform the conversion.
                bool success = doc.Convert(options);

                // If conversion failed, raise an exception to surface the error.
                if (!success)
                {
                    // The log file contains details; include it in the exception message.
                    string log = File.ReadAllText(logPath);
                    throw new ConvertException($"PDF/A‑3b conversion failed. Details:{Environment.NewLine}{log}");
                }

                // Save the converted document.
                doc.Save(outputPath);
                Console.WriteLine($"PDF/A‑3b file saved to '{outputPath}'.");
            }
        }
        catch (ConvertException ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}