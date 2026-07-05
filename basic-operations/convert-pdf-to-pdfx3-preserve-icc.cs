using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output PDF/X‑3 file path
        const string outputPdfX3 = "output_pdfx3.pdf";
        // Path for conversion log (optional, can be any writable location)
        const string logPath = "conversion_log.txt";

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
                // Configure conversion options for PDF/X‑3
                // No need to set IccProfileFileName – leaving it null preserves the
                // existing ICC profiles embedded in the source document.
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(
                    logPath,                // log file
                    PdfFormat.PDF_X_3,     // target format
                    ConvertErrorAction.Delete); // how to handle conversion errors

                // Perform the conversion
                bool success = doc.Convert(options);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failures. Check the log file for details.");
                }

                // Save the converted document as PDF/X‑3
                doc.Save(outputPdfX3);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X‑3: {outputPdfX3}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}