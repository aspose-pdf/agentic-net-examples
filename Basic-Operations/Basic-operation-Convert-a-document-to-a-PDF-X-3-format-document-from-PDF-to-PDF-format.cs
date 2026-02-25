using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";
        const string logPath = "conversion_log.xml";

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
                // Convert to PDF/X-3 format, logging any conversion errors
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as PDF/X-3
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X-3 and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}