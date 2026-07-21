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

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Prepare conversion options for PDF/X‑3.
                // Setting IccProfileFileName to null (default) preserves any existing ICC profile.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
                {
                    IccProfileFileName = null,   // keep existing ICC profile
                    OutputIntent      = null    // keep existing output intent if present
                };

                // Perform the conversion; errors are written to the log file.
                bool success = doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);
                // Alternatively: bool success = doc.Convert(options);

                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported errors; see log file.");
                }

                // Save the converted document as PDF/X‑3
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X‑3: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}