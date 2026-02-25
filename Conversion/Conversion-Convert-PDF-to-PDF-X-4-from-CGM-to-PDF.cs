using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputCgm = "input.cgm";
        const string outputPdfX4 = "output_pdfx4.pdf";
        const string logFile = "conversion_log.txt";

        if (!File.Exists(inputCgm))
        {
            Console.Error.WriteLine($"File not found: {inputCgm}");
            return;
        }

        try
        {
            // Load the CGM file (CGM is input‑only, no save options exist)
            CgmLoadOptions loadOptions = new CgmLoadOptions();
            using (Document doc = new Document(inputCgm, loadOptions))
            {
                // Convert the loaded document to PDF/X‑4 format
                doc.Convert(logFile, PdfFormat.PDF_X_4, ConvertErrorAction.Delete);

                // Save the resulting PDF/X‑4 file
                doc.Save(outputPdfX4);
            }

            Console.WriteLine($"Conversion completed: {outputPdfX4}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}