using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string psInputPath      = "input.ps";          // PostScript source file
        const string iccProfilePath   = "profile.icc";       // External ICC profile
        const string pdfOutputPath    = "output_pdfx4.pdf";  // Resulting PDF/X‑4 file
        const string conversionLog    = "conversion.log";    // Log file for conversion details

        // Verify that required files exist
        if (!File.Exists(psInputPath))
        {
            Console.Error.WriteLine($"PostScript file not found: {psInputPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the PS file using PsLoadOptions
            PsLoadOptions loadOptions = new PsLoadOptions();
            using (Document doc = new Document(psInputPath, loadOptions))
            {
                // Prepare conversion options for PDF/X‑4
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convOptions.IccProfileFileName = iccProfilePath;          // Use external ICC profile
                convOptions.LogFileName      = conversionLog;            // Optional: log conversion messages

                // Perform the conversion
                bool success = doc.Convert(convOptions);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failure (see log for details).");
                }

                // Save the resulting PDF/X‑4 document
                doc.Save(pdfOutputPath);
                Console.WriteLine($"Conversion completed. PDF/X‑4 saved to '{pdfOutputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}