using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "brochureF.pdf";
        const string outputPath = "brochureF_pdfx4.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF.
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/X-4 using the default output‑intent profile.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the converted document.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion successful. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}