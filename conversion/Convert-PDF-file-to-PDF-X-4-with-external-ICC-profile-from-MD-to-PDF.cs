using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // source PDF
        const string outputPdf  = "output_pdfx4.pdf"; // target PDF/X‑4
        const string iccProfile = "profile.icc";    // external ICC profile

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
            // Load the source PDF (lifecycle: create & load)
            using (Document doc = new Document(inputPdf))
            {
                // Configure conversion options for PDF/X‑4
                PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                options.IccProfileFileName = iccProfile; // external ICC profile

                // Perform the format conversion
                bool converted = doc.Convert(options);
                if (!converted)
                {
                    Console.Error.WriteLine("Conversion to PDF/X‑4 failed.");
                }

                // Save the converted document as PDF (lifecycle: save)
                doc.Save(outputPdf);
                Console.WriteLine($"PDF successfully converted to PDF/X‑4 and saved as '{outputPdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}