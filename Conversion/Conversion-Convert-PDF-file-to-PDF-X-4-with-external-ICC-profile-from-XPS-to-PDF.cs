using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xpsInputPath   = "input.xps";          // XPS source file
        const string pdfOutputPath  = "output_pdfx4.pdf";   // Resulting PDF/X‑4 file
        const string iccProfilePath = "profile.icc";        // External ICC profile

        // Verify files exist
        if (!File.Exists(xpsInputPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsInputPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the XPS document – XpsLoadOptions is the correct loader for XPS files
            using (Document doc = new Document(xpsInputPath, new XpsLoadOptions()))
            {
                // Prepare conversion options for PDF/X‑4 and attach the ICC profile
                PdfFormatConversionOptions convertOpts = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convertOpts.IccProfileFileName = iccProfilePath;

                // Perform the conversion
                bool success = doc.Convert(convertOpts);
                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failure.");
                    return;
                }

                // Save the converted document as PDF/X‑4
                doc.Save(pdfOutputPath);
                Console.WriteLine($"Conversion completed: {pdfOutputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}