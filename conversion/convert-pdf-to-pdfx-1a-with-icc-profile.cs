using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "brochureH.pdf";
        const string iccProfile = "profileI.icc";
        const string outputPdf  = "brochureH_pdfx1a.pdf";
        const string logFile    = "conversion_log.xml";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(iccProfile))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfile}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Attach the ICC profile as an OutputIntent – required for PDF/X compliance
                var outputIntent = new OutputIntent(iccProfile);
                doc.OutputIntents.Add(outputIntent);

                // Convert the document to PDF/X-1a:2001.
                // The log file captures any conversion errors.
                doc.Convert(logFile, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                // Save the resulting PDF/X-1a compliant file.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
