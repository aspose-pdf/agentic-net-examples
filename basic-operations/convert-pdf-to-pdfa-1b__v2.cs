using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa_1b.pdf";
        const string logPath   = "conversion_log.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF, convert it to PDF/A‑1b compliance, and save the result.
            using (Document doc = new Document(inputPdf))
            {
                // Convert to PDF/A‑1b; errors are logged to the specified file.
                doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
                // Save the converted document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/A‑1b compliant document saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}