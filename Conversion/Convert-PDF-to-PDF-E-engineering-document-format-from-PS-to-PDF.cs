using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PsLoadOptions (inherits from LoadOptions)

class Program
{
    static void Main()
    {
        const string psInputPath   = "input.ps";          // source PostScript file
        const string pdfOutputPath = "output.pdf";        // resulting PDF/E file
        const string logPath       = "conversion.log";   // optional log file

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PostScript file (input format) into a PDF document.
            using (Document doc = new Document())
            {
                // PsLoadOptions is the correct load option for PostScript input.
                doc.LoadFrom(psInputPath, new PsLoadOptions());

                // Convert the document to PDF/E (engineered PDF) format.
                // PDF/E is based on PDF/X‑3, therefore we use PdfFormat.PDF_X_3.
                // The Convert method also writes a log of any conversion issues.
                doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file.
                // The resulting file conforms to PDF/E (PDF/X‑3) specifications.
                doc.Save(pdfOutputPath);
            }

            Console.WriteLine($"Conversion completed. PDF/E saved to '{pdfOutputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}