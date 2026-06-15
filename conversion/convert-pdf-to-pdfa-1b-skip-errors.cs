using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa.pdf";
        const string logFile   = "conversion_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdf))
            {
                // Convert to PDF/A‑1b, skipping (i.e., not failing) non‑convertible elements
                // ConvertErrorAction.None corresponds to “skip” behavior
                doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.None);

                // Save the converted PDF/A document
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPdf}'. Log written to '{logFile}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}