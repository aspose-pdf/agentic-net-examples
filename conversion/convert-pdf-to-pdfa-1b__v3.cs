using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa.pdf";
        const string logPath = "conversion_log.xml";

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
                // Convert the document to PDF/A‑1b. Use ConvertErrorAction.Convert when available;
                // otherwise fall back to Delete (the older enum value) to skip problematic objects.
#if CONVERT_ERROR_ACTION_CONVERT_AVAILABLE
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Convert);
#else
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
#endif
                // Save the converted PDF/A document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A-1b and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
