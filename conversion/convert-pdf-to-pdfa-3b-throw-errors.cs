using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa3b.pdf";
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
                // Convert to PDF/A‑3b.  The Throw option is not available in older
                // Aspose.PDF versions, so we fall back to Delete which removes the
                // offending objects and still produces a PDF/A‑3b file.
#if CONVERT_ERROR_ACTION_THROW_AVAILABLE
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Throw);
#else
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
#endif
                // Save the converted PDF/A‑3b file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion succeeded: {outputPath}");
        }
        catch (Exception ex)
        {
            // Exceptions are raised when Throw is available; otherwise any
            // conversion problems are logged in the XML log file.
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
