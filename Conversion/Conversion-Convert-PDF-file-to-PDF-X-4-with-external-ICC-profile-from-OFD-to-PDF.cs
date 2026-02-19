using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input OFD file, external ICC profile and desired output PDF/X‑4 file
        string ofdPath = "input.ofd";
        string iccPath = "profile.icc";
        string outputPath = "output_pdfx4.pdf";

        // Verify that required files exist
        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }
        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        try
        {
            // Load the OFD document
            OfdLoadOptions loadOptions = new OfdLoadOptions();
            Document ofdDocument = new Document(ofdPath, loadOptions);

            // Prepare conversion options for PDF/X‑4 and assign the external ICC profile
            PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
            conversionOptions.IccProfileFileName = iccPath;

            // Convert the document to PDF/X‑4 (the method returns a bool indicating success)
            bool conversionResult = ofdDocument.Convert(conversionOptions);
            if (!conversionResult)
            {
                Console.Error.WriteLine("Conversion failed: Convert method returned false.");
                return;
            }

            // Save the resulting PDF/X‑4 file
            ofdDocument.Save(outputPath);
            Console.WriteLine($"Conversion succeeded. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}
