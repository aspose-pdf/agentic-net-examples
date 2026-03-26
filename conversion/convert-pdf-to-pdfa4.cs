using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa4.pdf";
        const string logPath = "conversion_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Attempt to convert to PDF/A‑4. If the current Aspose.PDF version supports
                // ConvertErrorAction.Convert, it will be used; otherwise we fall back to the
                // closest available action (Delete).
#if CONVERT_ERROR_ACTION_CONVERT_AVAILABLE
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Convert);
#else
                // Fallback for older library versions where "Convert" is not defined.
                // The enum does contain a Delete member, which removes problematic elements.
                doc.Convert(logPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);
#endif
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output: {outputPath}");
            Console.WriteLine($"Log written to: {logPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
