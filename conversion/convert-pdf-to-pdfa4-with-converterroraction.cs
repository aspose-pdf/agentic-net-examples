using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath    = "conversion_log.xml";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑4.
                // Use ConvertErrorAction.Convert when the enum member exists; otherwise fall back to Delete.
#if CONVERT_ERROR_ACTION_CONVERT_AVAILABLE
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Convert);
#else
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);
#endif
                // Save the converted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑4: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}