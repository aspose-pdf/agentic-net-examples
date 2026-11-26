using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "sourceK.pdf";
        const string outputPath = "sourceK_PDFX4.pdf";
        const string logPath = "conversion_log.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/X-4 format, logging any conversion errors.
                doc.Convert(logPath, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);
                // Save the converted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}