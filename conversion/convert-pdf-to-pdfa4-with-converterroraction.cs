using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.xml";

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
#if CONVERT_ERROR_ACTION_CONVERT_AVAILABLE
                // Newer Aspose.PDF versions expose ConvertErrorAction.Convert
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Convert);
#else
                // Older versions only support Delete as a fallback action
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);
#endif
                // Save the converted PDF/A‑4 document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑4. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
