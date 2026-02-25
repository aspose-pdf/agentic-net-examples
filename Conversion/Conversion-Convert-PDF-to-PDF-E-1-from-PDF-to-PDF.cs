using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfe1.pdf";
        const string logPath = "conversion_log.txt";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/E-1 format.
                // The conversion logs any issues to the specified log file.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_E_1, ConvertErrorAction.Delete);

                // Save the converted document. No SaveOptions needed because we are saving as PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion to PDF/E-1 completed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}