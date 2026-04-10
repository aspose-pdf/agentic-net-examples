using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa4.pdf";
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
                // Configure conversion options:
                // - Target format: PDF/A‑4
                // - ErrorAction: None (attempt conversion of problematic elements)
                PdfFormatConversionOptions convertOptions = new PdfFormatConversionOptions(
                    logFile,
                    PdfFormat.PDF_A_4,
                    ConvertErrorAction.None);

                // Perform the conversion
                bool success = doc.Convert(convertOptions);
                Console.WriteLine($"Conversion succeeded: {success}");

                // Save the converted PDF/A‑4 document
                doc.Save(outputPdf);
                Console.WriteLine($"Converted file saved to '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}