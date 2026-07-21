using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // PDF with ZUGFeRD attachment
        const string outputPath = "output_pdfa3u.pdf";  // Resulting PDF/A‑3U file
        const string logPath    = "conversion_log.txt"; // Log for conversion errors

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF (ZUGFeRD attachment is part of the document)
            using (Document doc = new Document(inputPath))
            {
                // Convert to PDF/A‑3U. The conversion keeps embedded files,
                // so the ZUGFeRD XML remains intact.
                bool ok = doc.Convert(logPath, PdfFormat.PDF_A_3U, ConvertErrorAction.Delete);
                if (!ok)
                {
                    Console.Error.WriteLine("Conversion reported errors – see log for details.");
                }

                // Save the converted document. Save() without options always writes PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF/A‑3U file created: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}