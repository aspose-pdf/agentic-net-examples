using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psInputPath   = "input.ps";          // source PostScript file
        const string pdfOutputPath = "output_pdfe1.pdf"; // target PDF/E‑1 file
        const string logPath       = "conversion_log.txt";

        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"Source file not found: {psInputPath}");
            return;
        }

        try
        {
            // Load the PS file using PsLoadOptions (PS is input‑only)
            using (Document doc = new Document(psInputPath, new PsLoadOptions()))
            {
                // Prepare conversion options: convert to PDF/E‑1, log errors, delete unconvertible objects
                var conversionOptions = new PdfFormatConversionOptions(
                    logPath,
                    PdfFormat.PDF_E_1,
                    ConvertErrorAction.Delete);

                // Perform the conversion; returns true if successful
                bool converted = doc.Convert(conversionOptions);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion completed with errors (see log).");
                }

                // Save the resulting PDF/E‑1 document
                doc.Save(pdfOutputPath);
                Console.WriteLine($"Successfully converted to PDF/E‑1: {pdfOutputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}