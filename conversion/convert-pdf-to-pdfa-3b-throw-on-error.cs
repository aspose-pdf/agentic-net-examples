using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfa3b.pdf";
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
                // Configure conversion to PDF/A‑3b.
                // ConvertErrorAction.None will cause conversion errors to raise exceptions.
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(logFile, PdfFormat.PDF_A_3B, ConvertErrorAction.None);

                // Perform the conversion. If an error occurs a ConvertException will be thrown.
                bool success = doc.Convert(convOptions);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion completed with errors (see log file).");
                }

                // Save the converted document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF successfully converted to PDF/A‑3b: {outputPdf}");
        }
        catch (ConvertException ex)
        {
            // Handle conversion errors that were thrown due to ConvertErrorAction.None
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}