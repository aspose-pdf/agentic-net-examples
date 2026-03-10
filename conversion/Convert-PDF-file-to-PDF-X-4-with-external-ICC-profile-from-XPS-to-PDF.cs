using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input XPS file path
        const string xpsPath = "input.xps";
        // Output PDF/X-4 file path
        const string pdfx4Path = "output_pdfx4.pdf";
        // External ICC profile file path (e.g., sRGB.icc)
        const string iccProfilePath = "profile.icc";

        // Verify that source files exist
        if (!File.Exists(xpsPath))
        {
            Console.Error.WriteLine($"XPS file not found: {xpsPath}");
            return;
        }
        if (!File.Exists(iccProfilePath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccProfilePath}");
            return;
        }

        try
        {
            // Load the XPS document using XpsLoadOptions
            using (Document doc = new Document(xpsPath, new XpsLoadOptions()))
            {
                // Prepare conversion options for PDF/X-4 with the external ICC profile
                PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4)
                {
                    IccProfileFileName = iccProfilePath
                };

                // Perform the conversion; the document is now in PDF/X-4 format
                doc.Convert(convOptions);

                // Save the converted document as PDF/X-4
                doc.Save(pdfx4Path);
            }

            Console.WriteLine($"Conversion completed. PDF/X-4 saved to '{pdfx4Path}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}