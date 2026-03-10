using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output.pdf";         // resulting PDF (PDF/A or PDF/X)
        const string logPath    = "conversion.log";    // conversion log file

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to a PDF/A or PDF/X format.
                // Choose the desired target format:
                //   PdfFormat.PDF_A_1B  – PDF/A‑1B compliance
                //   PdfFormat.PDF_X_3  – PDF/X‑3 compliance
                // ConvertErrorAction.Delete tells the engine to drop objects it cannot convert.
                bool success = doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                // If you need PDF/X instead, replace PdfFormat.PDF_A_1B with PdfFormat.PDF_X_3.

                if (!success)
                {
                    Console.WriteLine("Conversion completed with warnings. See the log file for details.");
                }

                // Save the converted document as a regular PDF file.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document successfully saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}