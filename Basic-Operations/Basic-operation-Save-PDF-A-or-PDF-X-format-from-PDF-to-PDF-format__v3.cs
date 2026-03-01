using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf"; // Change to PDF/X by using PdfFormat.PDF_X_3
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
                // Convert to PDF/A-1B (or PDF/X-3 by changing the PdfFormat value)
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the converted document (still a PDF file)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Converted document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}