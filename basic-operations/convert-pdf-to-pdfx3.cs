using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath    = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/X‑3 compliance.
                // This preserves embedded color profiles.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/X‑3 document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}