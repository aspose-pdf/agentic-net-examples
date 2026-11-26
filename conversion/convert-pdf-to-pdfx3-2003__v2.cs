using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "manualP.pdf";          // source PDF
        const string outputPath = "manualP_PDFX3.pdf";    // PDF/X-3:2003 compliant output
        const string logPath    = "conversion_log.xml";   // optional conversion log

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
                // Convert to PDF/X-3:2003. The enum value PdfFormat.PDF_X_3 represents this standard.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file (the conversion already enforces PDF/X-3 compliance)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. PDF/X-3 file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}