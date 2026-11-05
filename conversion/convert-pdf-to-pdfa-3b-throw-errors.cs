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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑3b. The Throw option does not exist in the
                // ConvertErrorAction enum, so we fall back to Delete which is the
                // safest available option for older Aspose.PDF versions.
                doc.Convert(logPath, PdfFormat.PDF_A_3B, ConvertErrorAction.Delete);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion succeeded. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
