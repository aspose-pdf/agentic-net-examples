using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";          // Source OFD file
        const string iccPath = "profile.icc";        // External ICC profile
        const string outputPdf = "output_pdfx4.pdf"; // Destination PDF/X‑4 file

        // Verify input files exist
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
            // Load OFD document (input format)
            using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
            {
                // Prepare conversion options for PDF/X‑4 and attach ICC profile
                var convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_4);
                convOptions.IccProfileFileName = iccPath;

                // Convert the document to PDF/X‑4
                doc.Convert(convOptions);

                // Save the converted PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion successful. PDF/X‑4 saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}