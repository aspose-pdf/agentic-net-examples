using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑1b, skipping any elements that cannot be converted.
                // Older Aspose.PDF versions do not expose ConvertErrorAction.Skip; they provide Delete instead.
                // The conditional compilation symbol CONVERT_ERROR_ACTION_SKIP_AVAILABLE should be defined
                // when the library version contains the Skip member.
#if CONVERT_ERROR_ACTION_SKIP_AVAILABLE
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Skip);
#else
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
#endif

                // Save the converted PDF/A‑1b document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑1b: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion error: {ex.Message}");
        }
    }
}
