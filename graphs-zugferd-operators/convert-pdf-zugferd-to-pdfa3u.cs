using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // PDF with ZUGFeRD attachment
        const string outputPath = "output_pdfa3u.pdf"; // Resulting PDF/A‑3U file
        const string logPath = "conversion_log.txt";   // Log for conversion details

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF (ZUGFeRD attachment is kept in the document)
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑3U. Attachments (including the ZUGFeRD XML) are preserved.
                // ConvertErrorAction.Delete removes objects that cannot be converted.
                doc.Convert(logPath, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);

                // Save the converted document as PDF/A‑3U.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3U file saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}