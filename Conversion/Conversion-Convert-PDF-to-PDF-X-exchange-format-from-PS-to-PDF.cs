using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per task requirement

class Program
{
    static void Main()
    {
        const string inputPsPath   = "input.ps";          // Source PostScript file
        const string outputPdfXPath = "output_pdfx.pdf";   // Destination PDF/X file
        const string logPath       = "conversion.log";    // Log file for conversion messages

        if (!File.Exists(inputPsPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPsPath}");
            return;
        }

        try
        {
            // Load the PostScript file using the concrete PsLoadOptions class.
            // PsLoadOptions derives from LoadOptions and provides the necessary
            // implementation for PS input format.
            using (Document doc = new Document(inputPsPath, new PsLoadOptions()))
            {
                // Convert the document to PDF/X‑1A format.
                // The log file will contain any conversion warnings or errors.
                doc.Convert(logPath, PdfFormat.PDF_X_1A, ConvertErrorAction.Delete);

                // Save the converted PDF/X document.
                doc.Save(outputPdfXPath);
            }

            Console.WriteLine($"Conversion succeeded. PDF/X saved to '{outputPdfXPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}