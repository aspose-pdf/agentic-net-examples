using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/A‑1b compliance
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                // Save the compliant PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑1b compliant document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}