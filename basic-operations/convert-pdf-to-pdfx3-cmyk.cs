using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.LogicalStructure; // not needed for this task but harmless

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_pdfx3.pdf";
        const string logFile   = "conversion_log.xml";

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
                // Convert to PDF/X‑3 format.
                // The overload with log file, target format and error action is the simplest way.
                // PDF/X‑3 requires CMYK color space; the conversion process will map colors accordingly.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF/X‑3 compliant file saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}